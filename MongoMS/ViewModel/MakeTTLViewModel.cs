using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("ttl")]
    [CommandLevel(Level.Collection)]
    internal class MakeTTLViewModel : CollectionVMB
    {
        public MakeTTLViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
        }
    }
}