using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
namespace MongoMS.Common
{
    public class CollectionOKViewModel : OKViewModel
    {
        protected readonly MongoCollection Collection;
        public CollectionOKViewModel(MongoCollection collection)
        {
            Collection = collection;
            FieldNames = GetFieldNames();
        }
        private IEnumerable<string> GetFieldNames()
        {
            var _coll = Collection as MongoCollection<BsonDocument>;
            MongoCursor<BsonDocument> cur = _coll.FindAll().SetLimit(100);
            IEnumerable<string> fields = new List<string>();
            foreach (BsonDocument doc in cur)
            {
                fields = fields.Union(doc.Names);
            }
            return fields;
        }
        public IEnumerable<string> FieldNames { get; private set; }
    }
}
