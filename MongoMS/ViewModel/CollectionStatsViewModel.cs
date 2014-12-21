using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Статистика")]
    [CollectionLevelCommand]
    internal class CollectionStatsViewModel : VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;

        private CollectionStatsResult stats;

        public CollectionStatsViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;

            stats = _coll.GetStats();
        }
    }
}