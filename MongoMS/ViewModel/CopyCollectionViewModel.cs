using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    internal class CopyCollectionViewModel:CollectionVMB
    {
        public CopyCollectionViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
        }

    }
}