using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("������ ������")]
    internal class EditRecordViewModel
    {
        private readonly MongoCollection<BsonDocument> _coll;
        private readonly BsonDocument _selected;

        public EditRecordViewModel(MongoCollection<BsonDocument> coll, BsonDocument selected)
        {
            _coll = coll;
            _selected = selected;
        }
    }
}