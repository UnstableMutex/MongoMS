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

        private string GetSchema(string tablename)
        {
            var dot = ".";
            return tablename.Substring(0, tablename.IndexOf(dot));
        }
        private string GetTable(string tablename)
        {
            var dot = ".";
            return tablename.Substring(tablename.IndexOf(dot)+1);
        }

        int getschemaid(string name)
        {
             using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select schema_id from sys.schemas where name='" + name + "'";
                    return (int) cmd.ExecuteScalar();
                }
            }
        }

        int gettableid(string name, int schid)
        {

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select object_id from sys.tables where name='"+name+"' and schema_id="+schid;
                    return (int) cmd.ExecuteScalar();
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
