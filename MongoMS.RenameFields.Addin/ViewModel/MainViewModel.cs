using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.RenameFields.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public MainViewModel(MongoCollection collection)
        {
            _collection = collection;
            FieldNames = GetFieldNames();
        }

        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
        public virtual IEnumerable<string> FieldNames { get; private set; }
        private IEnumerable<string> GetFieldNames()
        {
            var _coll = _collection as MongoCollection<BsonDocument>;
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
