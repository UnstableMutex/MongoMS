using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    internal class CollectionVMB : OKVMB
    {
        protected readonly MongoCollection<BsonDocument> _coll;

        public CollectionVMB(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            FieldNames = GetFieldNames();
        }

        public virtual IEnumerable<string> FieldNames { get; private set; }

        private IEnumerable<string> GetFieldNames()
        {
            MongoCursor<BsonDocument> cur = _coll.FindAll().SetLimit(100);
            IEnumerable<string> fields = new List<string>();
            foreach (BsonDocument doc in cur)
            {
                fields = fields.Union(doc.Names);
            }
            return fields;
        }
    }
}