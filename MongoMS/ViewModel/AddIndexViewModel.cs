using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class AddIndexViewModel:VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;

        public AddIndexViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
        }
    }
}
