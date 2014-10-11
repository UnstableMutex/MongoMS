using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerDBViewModel
    {
        private readonly string _cs;
        private readonly string _db;

        public DatabaseExplorerDBViewModel(string cs,string db)
        {
            _cs = cs;
            _db = db;
        }
        public string Name { get { return _db; } }
    }
}
