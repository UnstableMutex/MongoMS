using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class IndexInfo:IEquatable<IndexInfo>
    {

        public string Name { get; set; }
        public string Field { get; set; }
        public sbyte Order { get; set; }
        public bool Equals(IndexInfo other)
        {
            return Field == other.Field && Order == other.Order;
        }

        public override bool Equals(object obj)
        {
            return Equals((IndexInfo) obj);
        }

    }
    [Header("Добавить индекс")]
    class AddIndexViewModel : VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;
        private IEnumerable<string> _fieldNames;

        public AddIndexViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            var fns = GetFieldNames();
            var ind = coll.GetIndexes();

            Indexes = new PairedObservableCollections<string>(fns);
        }

        public ICommand OKCommand { get; private set; }

        private void OK()
        {

        }
        public PairedObservableCollections<string> Indexes { get; private set; }
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


    }
}
