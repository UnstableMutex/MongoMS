using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("ttl")]
    internal class MakeTTLViewModel : CollectionVMB
    {
        public MakeTTLViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
        }
    }
}