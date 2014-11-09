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
            _tableNames = new Lazy<IEnumerable<string>>(()=>GetTableNames().OrderBy(x=>x));
        }

        private Lazy<IEnumerable<string>> _tableNames;
        public IEnumerable<string> TableNames { get { return _tableNames.Value; } }
        public string PrimaryTable { get; set; }
        public string SecondaryTable { get; set; }
        public bool SaveSecondaryKeys { get; set; }
        protected override void OK()
        {
            var fk = GetFKColumnName();
            var foreigntablepk = getpk(SecondaryTable);
            var pk = GetPKColumnName();
            var stname = SecondaryTable.Substring(0, SecondaryTable.IndexOf("."));
            var ptpk = getpk(PrimaryTable);
            using (var sconn = new SqlConnection(ConnectionString))
            using (var pconn = new SqlConnection(ConnectionString))
            {
                sconn.Open(); pconn.Open();
                using (var scmd = sconn.CreateCommand())
                using (var pcmd = pconn.CreateCommand())
                {

                    const string cmdtext = "select * from {0} where {1} is not null order by {1} desc";
                    pcmd.CommandText = string.Format(cmdtext, PrimaryTable, pk);
                    scmd.CommandText = string.Format(cmdtext, SecondaryTable, fk);
                    using (var pr = pcmd.ExecuteReader())
                    using (var sr = pcmd.ExecuteReader())
                    {

                        Dictionary<object, List<BsonDocument>> dic = new Dictionary<object, List<BsonDocument>>();
                        while (sr.Read())
                        {
                            var doc = GetDocuementFromRecord(sr, foreigntablepk);
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

                            BsonArray arr = new BsonArray(dic[pr[pk]]);
                            doc.Add(stname, arr);

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






        public string GetFKColumnName()
        {

            var stid = GetTableID(SecondaryTable);

            int scolid;


            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select referenced_column_id from sys.foreign_key_columns where referenced_object_id=" + stid;
                    scolid = (int)cmd1.ExecuteScalar();
                }
            }


            string fkcolname;
            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select name from sys.columns where object_id=" + stid + " and column_id=" + scolid;
                    fkcolname = (string)cmd1.ExecuteScalar();
                }
            }


            return fkcolname;

        }

        public string GetPKColumnName()
        {

            var ptidd = GetTableID(PrimaryTable);
            int pcolid;


            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select parent_column_id  from sys.foreign_key_columns where parent_object_id=" + ptidd;
                    pcolid = (int)cmd1.ExecuteScalar();
                }
            }





            string pkcolname;
            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select name from sys.columns where object_id=" + ptidd + " and column_id=" + pcolid;
                    pkcolname = (string)cmd1.ExecuteScalar();
                }
            }

            return pkcolname;

        }

    }
}
