using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class IndexInfo : IEquatable<IndexInfo>
    {

        public string Name { get; set; }
        public string Field { get; set; }
        public sbyte Order { get; set; }
        public bool Equals(IndexInfo other)
        {
            return Field == other.Field && Order == other.Order;
        }

        public override int GetHashCode()
        {
            return Field.GetHashCode() * Order.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((IndexInfo)obj);
        }

    }
    [Header("Добавить индекс")]
    class AddIndexViewModel : VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;
        private IEnumerable<string> _fieldNames;
        private string _selectedField;
        private string _indexName;
        private sbyte _direction;

        public AddIndexViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            FieldNames = GetFieldNames();
            ExistsIndexes = new ObservableCollection<IndexInfo>(GetExistsIndexes());
            Direction = 1;
            AssignCommands<NoWeakRelayCommand>();
        }

        private IEnumerable<IndexInfo> GetExistsIndexes()
        {
            var res = _coll.GetIndexes();
            foreach (var ind in res)
            {
                var ii = new IndexInfo();
                var key = ind.Key.Single();
                ii.Name = ind.Name;
                ii.Field = key.Name;
                ii.Order = sbyte.Parse(key.Value.ToString());
                yield return ii;
            }
        }
        public ObservableCollection<IndexInfo> ExistsIndexes { get; private set; }


        public ICommand OKCommand { get; private set; }

        private void OK()
        {
            IndexKeysBuilder b = new IndexKeysBuilder();
            b.Ascending(SelectedField);
            IndexOptionsBuilder b1=new IndexOptionsBuilder();
            b1.SetName(IndexName);
            _coll.CreateIndex(b,b1);
            ExistsIndexes = new ObservableCollection<IndexInfo>(GetExistsIndexes());
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

        public string IndexName
        {
            get { return _indexName; }
            set
            {
                _indexName = value;
                RaisePropertyChangedNoSave();
            }
        }

        public sbyte Direction
        {
            get { return _direction; }
            set
            {
                _direction = value; 
                RaisePropertyChangedNoSave();
                ChangeIndexName();
            }
        }

        public ICommand ChangeDirectionCommand { get; private set; }

        private void ChangeDirection()
        {
            Direction *= -1;
        }

        public string SelectedField
        {
            get { return _selectedField; }
            set
            {
                _selectedField = value; 
                RaisePropertyChangedNoSave();
                ChangeIndexName();
              
            }
        }

        private void ChangeIndexName()
        {
            IndexName = SelectedField + (Direction == 1 ? "Asc" : "Desc") + "Index";
        }
    }
}
