using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("Объединить таблицы")]
    class JoinSQLTablesViewModel : MSSQLViewModelBase
    {

        public JoinSQLTablesViewModel(MongoDatabase db)
            : base(db)
        {
            _tableNames = new Lazy<IEnumerable<string>>(GetTableNames);
        }

        private Lazy<IEnumerable<string>> _tableNames;
        public IEnumerable<string> TableNames { get { return _tableNames.Value; } }
        public string PrimaryTable { get; set; }
        public string SecondaryTable { get; set; }
        public bool SaveSecondaryKeys { get; set; }
        protected override void OK()
        {
            var fk = GetFKName();
            using (var sconn = new SqlConnection(ConnectionString))
            using (var pconn = new SqlConnection(ConnectionString))
            {
                sconn.Open(); pconn.Open();
                using (var scmd = sconn.CreateCommand())
                using (var pcmd = pconn.CreateCommand())
                {

                    const string cmdtext = "select * from {0}";
                    pcmd.CommandText = string.Format(cmdtext, PrimaryTable);
                    scmd.CommandText = string.Format(cmdtext, SecondaryTable);
                    using (var pr = pcmd.ExecuteReader())
                    using (var sr = pcmd.ExecuteReader())
                    {

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

        int GetFKID(string pt, string st)
        {
            var stid = GetTableID(SecondaryTable);
            var ptid = GetTableID(PrimaryTable);


            using (var conn1 = new SqlConnection(ConnectionString))
            {
                conn1.Open();
                using (var cmd1 = conn1.CreateCommand())
                {
                    cmd1.CommandText = "select object_id from sys.foreign_keys where parent_object_id=" + ptid + " and referenced_object_id=" + stid;
                    return (int)cmd1.ExecuteScalar();
                }
            }


        }




        public string GetFKName()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {

            }
        }

    }
}
