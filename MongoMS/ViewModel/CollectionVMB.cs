using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoMS.ViewModel
{
    class CollectionVMB:OKVMB
    {
        protected readonly MongoCollection<BsonDocument> _coll;

        public CollectionVMB(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            FieldNames = GetFieldNames();
        }
        private IEnumerable<string> GetFieldNames()
        {
            var cur = _coll.FindAll().SetLimit(100);
            IEnumerable<string> fields = new List<string>();
            foreach (var doc in cur)
            {
                fields = fields.Union(doc.Names);
            }
            return fields;
        }
        public IEnumerable<string> FieldNames { get; private set; }
      
    }
}
