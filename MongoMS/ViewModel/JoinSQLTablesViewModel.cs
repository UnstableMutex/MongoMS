using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("Объединить таблицы")]
    class JoinSQLTablesViewModel : MSSQLViewModelBase
    {

        public JoinSQLTablesViewModel(MongoDatabase db)
            : base(db)
        {
            _tableNames = new Lazy<IEnumerable<string>>(() => GetTableNames().OrderBy(x => x));
        }

        private Lazy<IEnumerable<string>> _tableNames;
        public IEnumerable<string> TableNames { get { return _tableNames.Value; } }
        public string PrimaryTable { get; set; }
        public string SecondaryTable { get; set; }
        public bool SaveSecondaryKeys { get; set; }
        protected override bool CanOK()
        {
            return !(string.IsNullOrEmpty(SecondaryTable) || string.IsNullOrEmpty(PrimaryTable));
        }

        protected override void OK()
        {
            var coll = _db.GetCollection(PrimaryTable);
            var fk = GetFKColumnName();
            var foreigntablepk = getpk(SecondaryTable);
            var pk = GetPKColumnName();
            var stname = SecondaryTable.Substring(SecondaryTable.IndexOf(".")+1);
            var ptpk = getpk(PrimaryTable);
            using (var sconn = new SqlConnection(ConnectionString))
            using (var pconn = new SqlConnection(ConnectionString))
            {
                sconn.Open(); pconn.Open();
                using (var scmd = sconn.CreateCommand())
                using (var pcmd = pconn.CreateCommand())
                {

                    const string cmdtext = "select * from {0} where {1} is not null order by {1}";
                    pcmd.CommandText = string.Format(cmdtext, PrimaryTable, pk);
                    scmd.CommandText = string.Format(cmdtext, SecondaryTable, fk);
                    using (var pr = pcmd.ExecuteReader())
                    using (var sr = scmd.ExecuteReader())
                    {

                        SortedDictionary<object, List<BsonDocument>> dic = new SortedDictionary<object, List<BsonDocument>>();
                        while (sr.Read())
                        {
                            var doc = GetDocuementFromRecord(sr, foreigntablepk);
                            if (!SaveSecondaryKeys)
                            {
                                doc.Remove(fk);
                            }
                            var key = sr[fk];
                            if (dic.ContainsKey(key))
                            {
                                dic[key].Add(doc);
                            }
                            else
                            {
                                dic.Add(key, new List<BsonDocument>() { doc });
                            }
                        }

                        while (pr.Read())
                        {
                            var doc = GetDocuementFromRecord(pr, ptpk);
                            var key = pr[pk];

                            try
                            {
                                BsonArray arr = new BsonArray(dic[key]);
                                doc.Add(stname, arr);
                            }
                            catch (KeyNotFoundException e)
                            {
                                //Console.WriteLine(e);
                            }

                            coll.Save(doc);
                        }

                    }
                }
            }
        }


        public int GetTableID(string tableName)
        {
            int schid;
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var dot = ".";
                    cmd.CommandText = "select schema_id from sys.schemas where name='" + tableName.Substring(0, tableName.IndexOf(dot)) + "'";
                    schid = (int)cmd.ExecuteScalar();
                }
            }

            var dot1 = ".";
            var tablen = tableName.Substring(tableName.IndexOf(dot1) + 1);
            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select object_id from sys.tables where name='" + tablen + "' and schema_id=" + schid;
                    return (int)cmd1.ExecuteScalar();
                }
            }
        }

        public int GetFKID(string pt, string st)
        {
            var stid = GetTableID(st);
            var ptid = GetTableID(pt);
            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select object_id from sys.foreign_keys where  referenced_object_id=" + ptid + " and parent_object_id=" + stid;
                    return (int)cmd1.ExecuteScalar();
                }
            }
        }

        string getcolname(int tid, int cid)
        {
            string q = "select name from sys.columns where object_id=" + tid + " and column_id=" + cid;
            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = q;
                    return (string)cmd1.ExecuteScalar();
                }
            }

        }




        public string GetFKColumnName()
        {

            //   var stid = GetTableID(SecondaryTable);
            string fkcolname = "";

            var fkid = GetFKID(PrimaryTable, SecondaryTable);

            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select parent_object_id, parent_column_id from sys.foreign_key_columns where  constraint_object_id=" + fkid;
                    using (var r = cmd1.ExecuteReader())
                    {
                        r.Read();
                        var tbl = r.GetInt32(0);
                        var col = r.GetInt32(1);
                        return getcolname(tbl, col);

                    }
                }
            }





        }



        public string GetPKColumnName()
        {

            string pkcolname = "";
            // var ptidd = GetTableID(PrimaryTable);
            var fkid = GetFKID(PrimaryTable, SecondaryTable);

            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select referenced_object_id, referenced_column_id from sys.foreign_key_columns where  constraint_object_id=" + fkid;
                    using (var r = cmd1.ExecuteReader())
                    {
                        r.Read();
                        var tbl = r.GetInt32(0);
                        var col = r.GetInt32(1);
                        return getcolname(tbl, col);

                    }
                }
            }






            return pkcolname;

        }

    }
}
