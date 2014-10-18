using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class FindViewModel:VMB
    {
        private readonly string _cs;
        private readonly string _db;
        private readonly string _coll;

        public FindViewModel(string cs,string db,string coll)
        {
            _cs = cs;
            _db = db;
            _coll = coll;
        }
    }
}
