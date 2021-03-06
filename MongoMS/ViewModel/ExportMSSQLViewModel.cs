﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [CommandLevel(Level.Database)]
    internal class MSSQLViewModelBase : OKVMB
    {
        protected MongoDatabase _db;

        public MSSQLViewModelBase(MongoDatabase db)
        {
            _db = db;
            AssignCommands<NoWeakRelayCommand>();
            ConnectionString = "Server=MainPC;Database=AdventureWorks2014;Trusted_Connection=True;";
        }

        public string ConnectionString { get; set; }

        protected IEnumerable<string> GetTableNames()
        {
            string q = "SELECT TABLE_SCHEMA+'.'+TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
            var tablenames = new HashSet<string>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            tablenames.Add(r.GetString(0));
                        }
                    }
                }
            }
            return tablenames;
        }

        protected BsonValue ConvertToBsonValue(object field, Type fieldType)
        {
            string s = field.ToString();
            switch (fieldType.Name)
            {
                case "String":
                    return new BsonString(s);
                    break;
                case "Boolean":
                    return (bool) field;
                    break;
                case "Int32":

                    return new BsonInt32(int.Parse(s));
                    break;
                case "Int16":
                    return new BsonInt32((short) field);
                case "DateTime":
                    return new BsonDateTime((DateTime) field);
                case "Byte":
                    return new BsonInt32((Byte) field);
                case "Byte[]":
                    return new BsonBinaryData((byte[]) field);
                case "TimeSpan":
                    return new BsonInt64(((TimeSpan) field).Ticks);
                case "Guid":
                    return (Guid) field;
                case "Decimal":
                    return ((decimal) field).ToString("F4");
                case "SqlHierarchyId":
                case "SqlGeography":
                    throw new NotSupportedException();
                default:

                    throw new Exception();
            }
        }

        protected string getpk(string tablename)
        {
            string q = @"SELECT COLUMN_NAME from 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
    INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
WHERE 
    Col.Constraint_Name = Tab.Constraint_Name
    AND Col.Table_Name = Tab.Table_Name
    AND Constraint_Type = 'PRIMARY KEY'
    AND Col.TABLE_SCHEMA+'.'+Col.Table_Name = '" + tablename + "'";
            string key = string.Empty;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            key = r.GetString(0);
                        }
                    }
                }
            }
            return key;
        }

        protected BsonDocument GetDocuementFromRecord(SqlDataReader r, string pk)
        {
            BsonDocument doc;
            doc = new BsonDocument();
            for (int i = 0; i < r.FieldCount; i++)
            {
                string n = r.GetName(i);
                if (n == pk)
                {
                    doc["_id"] = ConvertToBsonValue(r[i], r.GetFieldType(i));
                }
                else
                {
                    if (!r.IsDBNull(i))
                    {
                        doc[n] = ConvertToBsonValue(r[i], r.GetFieldType(i));
                    }
                }
            }
            return doc;
        }
    }

    internal class ExportMSSQLViewModel : MSSQLViewModelBase
    {
        public ExportMSSQLViewModel(MongoDatabase db) : base(db)
        {
        }

        protected override void OK()
        {
            IEnumerable<string> tables = GetTableNames();
            foreach (string table in tables)
            {
                ImportTable(table);
            }
        }

        private void ImportTable(string table)
        {
            try
            {
                ImportTableImpl(table);
            }
            catch (NotSupportedException e)
            {
                // Console.WriteLine(e);
            }
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        private void ImportTableImpl(string table)
        {
            string q = string.Format("select * from {0}", table);
            string pk = getpk(table);

            MongoCollection<BsonDocument> coll = _db.GetCollection(table);

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        BsonDocument doc;
                        while (r.Read())
                        {
                            doc = GetDocuementFromRecord(r, pk);
                            coll.Save(doc);
                        }
                    }
                }
            }
        }
    }
}