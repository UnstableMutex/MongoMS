using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseDetailsViewModel:VMB
    {
        private readonly MongoDatabase _db;


        public DatabaseDetailsViewModel(MongoDatabase db)
        {
            _db = db;
        }
    }
}
