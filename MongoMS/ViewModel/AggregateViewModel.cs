using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Агрегация")]
    internal class AggregateViewModel : VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;

        public AggregateViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
        }
    }
}