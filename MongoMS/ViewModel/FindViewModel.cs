using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;
namespace MongoMS.ViewModel
{
    [Header("Поиск")]
    class FindViewModel : VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;
        private IEnumerable<BsonDocument> _queryResults;
        private BsonDocument _selected;
        private string _findCriteria;
        public FindViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            QueryResults = _coll.FindAll().SetLimit(100).ToList();
            AssignCommands<NoWeakRelayCommand>();
        }
        public ICommand EditCommand { get; private set; }
        void Edit()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new EditRecordViewModel(_coll, Selected));
        }
        public ICommand FindCommand { get; private set; }
        void Find()
        {
            var doc = new QueryDocument(BsonDocument.Parse(FindCriteria));
            QueryResults = _coll.Find(doc);
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
