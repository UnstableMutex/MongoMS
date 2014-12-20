using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
     [Header("ttl")]
    class MakeTTLViewModel:CollectionVMB
    {
         public MakeTTLViewModel(MongoCollection<BsonDocument> coll) : base(coll)
         {
         }
    }
}
