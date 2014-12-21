using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("ttl")]
    [CollectionLevelCommand]
    internal class MakeTTLViewModel : CollectionVMB
    {
        public MakeTTLViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
        }
    }
}