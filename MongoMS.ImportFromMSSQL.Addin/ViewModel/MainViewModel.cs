using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.ImportFromMSSQL.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoDatabase _database;

        public MainViewModel(MongoDatabase database)
        {
            _database = database;
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
                    return (bool)field;
                    break;
                case "Int32":

                    return new BsonInt32(int.Parse(s));
                    break;
                case "Int16":
                    return new BsonInt32((short)field);
                case "DateTime":
                    return new BsonDateTime((DateTime)field);
                case "Byte":
                    return new BsonInt32((Byte)field);
                case "Byte[]":
                    return new BsonBinaryData((byte[])field);
                case "TimeSpan":
                    return new BsonInt64(((TimeSpan)field).Ticks);
                case "Guid":
                    return (Guid)field;
                case "Decimal":
                    return ((decimal)field).ToString("F4");
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
}
