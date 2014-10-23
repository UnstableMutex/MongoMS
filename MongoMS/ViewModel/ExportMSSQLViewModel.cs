﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class ExportMSSQLViewModel : VMB
    {
        private readonly MongoDatabase _db;

        public ExportMSSQLViewModel(MongoDatabase db)
        {
            _db = db;
            AssignCommands<NoWeakRelayCommand>();
            ConnectionString = "Server=MainPC;Database=test;Trusted_Connection=True;";
        }
        public string ConnectionString { get; set; }
        public ICommand OKCommand { get; private set; }

        IEnumerable<string> GetTableNames()
        {
            string q = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
            var tablenames = new HashSet<string>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (var r = cmd.ExecuteReader())
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
        void OK()
        {

            var tables = GetTableNames();
            foreach (var table in tables)
            {
                ImportTable(table);
            }
        }

        private void ImportTable(string table)
        {
            string q = string.Format("select * from {0}", table);
            var pk = getpk(table);
            
            var coll = _db.GetCollection(table);

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (var r = cmd.ExecuteReader())
                    {
                        BsonDocument doc;
                        while (r.Read())
                        {
                            doc=new BsonDocument();
                            for (int i = 0; i < r.FieldCount; i++)
                            {
                                var n = r.GetName(i);
                                if (n == pk)
                                {
                                    doc["_id"] = r.GetInt32(i);
                                }
                                else
                                {
                                    doc[n] = r.GetString(i);
                                }

                            }
                            coll.Save(doc);
                        }

                    }
                }
            }
            //return key;
        }

        string getpk(string tablename)
        {
            string q = @"SELECT Col.Column_Name from 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
    INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
WHERE 
    Col.Constraint_Name = Tab.Constraint_Name
    AND Col.Table_Name = Tab.Table_Name
    AND Constraint_Type = 'PRIMARY KEY'
    AND Col.Table_Name = 'Table_1'";
            string key = string.Empty;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = q;
                    using (var r = cmd.ExecuteReader())
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
    }
}