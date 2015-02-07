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
    internal class IndexInfo : IEquatable<IndexInfo>
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
            return Field.GetHashCode()*Order.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals((IndexInfo) obj);
        }
    }

    [Header("Добавить индекс")]
    [CommandLevel(Level.Collection)]
    internal class AddIndexViewModel : CollectionVMB
    {
        private sbyte _direction;
        private string _indexName;
        private string _selectedField;
        private ObservableCollection<IndexInfo> _existsIndexes;

        public AddIndexViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
            ExistsIndexes = new ObservableCollection<IndexInfo>(GetExistsIndexes());
            Direction = 1;
            AssignCommands<NoWeakRelayCommand>();
        }

        public ObservableCollection<IndexInfo> ExistsIndexes
        {
            get { return _existsIndexes; }
            private set
            {
                _existsIndexes = value;
                RaisePropertyChanged();
            }
        }


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

        private IEnumerable<IndexInfo> GetExistsIndexes()
        {
            GetIndexesResult res = _coll.GetIndexes();
            foreach (MongoDB.Driver.IndexInfo ind in res)
            {
                var ii = new IndexInfo();
                BsonElement key = ind.Key.Single();
                ii.Name = ind.Name;
                ii.Field = key.Name;
                ii.Order = sbyte.Parse(key.Value.ToString());
                yield return ii;
            }
        }

        protected override void OK()
        {
            var b = new IndexKeysBuilder();
            b.Ascending(SelectedField);
            var b1 = new IndexOptionsBuilder();
            b1.SetName(IndexName);
            _coll.CreateIndex(b, b1);

            ExistsIndexes = new ObservableCollection<IndexInfo>(GetExistsIndexes());
        }

        private void ChangeDirection()
        {
            Direction *= -1;
        }

        private void ChangeIndexName()
        {
            IndexName = SelectedField + (Direction == 1 ? "Asc" : "Desc") + "Index";
        }
    }
}