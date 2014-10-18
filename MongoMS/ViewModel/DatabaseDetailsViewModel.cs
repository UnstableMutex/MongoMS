using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseDetailsViewModel:VMB
    {
        private readonly string _cs;
        private readonly string _dbname;

        public DatabaseDetailsViewModel(string cs,string dbname)
        {
            _cs = cs;
            _dbname = dbname;
        }

    }
}
