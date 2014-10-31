using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MVVMLight.Extras;
namespace MongoMS.ViewModel
{
    class SortParameterViewModel : VMB
    {
        public SortParameterViewModel(string fieldName)
        {
            AssignCommands<NoWeakRelayCommand>();
            FieldName = fieldName;


        }
        private OrderByDirection _currentDirection;
        public string FieldName { get; private set; }
        public ICommand ChangeDirectionCommand { get; set; }

        void ChangeDirection()
        {
            CurrentDirection = CurrentDirection == OrderByDirection.Ascending ? OrderByDirection.Descending : OrderByDirection.Ascending;
        }


        public OrderByDirection CurrentDirection
        {
            get { return _currentDirection; }
            set
            {
                _currentDirection = value;
                RaisePropertyChangedNoSave();
            }
        }
    }
    [Header("Поиск")]
    class FindViewModel : CollectionVMB
    {
        // private readonly MongoCollection<BsonDocument> _coll;
        private IEnumerable<BsonDocument> _queryResults;
        private BsonDocument _selected;
        private string _findCriteria;
        private IEnumerable<string> _fieldNames;

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

        private void AddFieldToSort()
        {
            FieldsToSort.Add(new SortParameterViewModel(SelectedFieldToSort));
        }
        public ObservableCollection<SortParameterViewModel> FieldsToSort { get; set; }

        public ICommand EditCommand { get; private set; }
        void Edit()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new EditRecordViewModel(_coll, Selected));
        }
        // public ICommand FindCommand { get; private set; }
        protected override void OK()
        {
            BsonDocument d = string.IsNullOrEmpty(FindCriteria) ? new BsonDocument() : BsonDocument.Parse(FindCriteria);
            SortByDocument sbd = new SortByDocument();
            foreach (var sort in FieldsToSort)
            {
                sbd.Add(sort.FieldName, sort.CurrentDirection == OrderByDirection.Ascending ? 1 : -1);
            }
            var doc = new QueryDocument(d);
            QueryResults = _coll.Find(doc).SetSortOrder(sbd).SetFields(FieldsToView.SelectedItems.ToArray());
        }
        public string FindCriteria
        {
            get { return _findCriteria; }
            set { _findCriteria = value; }
        }
        public BsonDocument Selected
        {
            get { return _selected; }
            set
            {
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
    }
}
