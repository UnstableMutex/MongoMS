using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    internal class SortParameterViewModel : VMB
    {
        private OrderByDirection _currentDirection;

        public SortParameterViewModel(string fieldName)
        {
            AssignCommands<NoWeakRelayCommand>();
            FieldName = fieldName;
        }

        public string FieldName { get; private set; }
        public ICommand ChangeDirectionCommand { get; set; }


        public OrderByDirection CurrentDirection
        {
            get { return _currentDirection; }
            set
            {
                _currentDirection = value;
                RaisePropertyChangedNoSave();
            }
        }

        private void ChangeDirection()
        {
            CurrentDirection = CurrentDirection == OrderByDirection.Ascending
                ? OrderByDirection.Descending
                : OrderByDirection.Ascending;
        }
    }

    [Header("Поиск")]
    [CollectionLevelCommand]
    internal class FindViewModel : CollectionVMB
    {
        // private readonly MongoCollection<BsonDocument> _coll;
        private IEnumerable<string> _fieldNames;
        private IEnumerable<BsonDocument> _queryResults;
        private BsonDocument _selected;

        public FindViewModel(MongoCollection<BsonDocument> coll)
            : base(coll)
        {
            QueryResults = _coll.FindAll().SetLimit(100).ToList();

            AssignCommands<NoWeakRelayCommand>();

            FieldsToView = new PairedObservableCollections<string>(FieldNames);

            FieldsToSort = new ObservableCollection<SortParameterViewModel>();
        }

        public PairedObservableCollections<string> FieldsToView { get; private set; }


        // public string SelectedFieldToView { get; set; }
        public string SelectedFieldToSort { get; set; }

        public ICommand AddFieldToSortCommand { get; private set; }

        public ObservableCollection<SortParameterViewModel> FieldsToSort { get; set; }

        public ICommand EditCommand { get; private set; }

        public string FindCriteria { get; set; }

        public BsonDocument Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != null)
                {
                    _coll.Save(_selected);
                }
                _selected = value;

                RaisePropertyChangedNoSave();
            }
        }

        public IEnumerable<BsonDocument> QueryResults
        {
            get { return _queryResults; }
            set
            {
                _queryResults = value;
                RaisePropertyChangedNoSave();
            }
        }

        private void AddFieldToSort()
        {
            FieldsToSort.Add(new SortParameterViewModel(SelectedFieldToSort));
        }

        private void Edit()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new EditRecordViewModel(_coll, Selected));
        }

        protected override void OK()
        {
            BsonDocument d = string.IsNullOrEmpty(FindCriteria) ? new BsonDocument() : BsonDocument.Parse(FindCriteria);
            var sbd = new SortByDocument();
            foreach (SortParameterViewModel sort in FieldsToSort)
            {
                sbd.Add(sort.FieldName, sort.CurrentDirection == OrderByDirection.Ascending ? 1 : -1);
            }
            var doc = new QueryDocument(d);
            QueryResults = _coll.Find(doc).SetSortOrder(sbd).SetFields(FieldsToView.SelectedItems.ToArray()).ToList();
        }
    }
}