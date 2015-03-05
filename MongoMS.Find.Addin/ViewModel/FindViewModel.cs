using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.Find.Addin.ViewModel
{
    public class FindViewModel:CollectionOKViewModel
    {
        private IEnumerable<BsonDocument> _queryResults;

        public FindViewModel(MongoCollection collection):base(collection)
        {
            AddFieldToSortCommand = new DelegateCommand(AddFieldToSort);
            FieldsToSort=new ObservableCollection<string>();
            FieldsToView = new ObservableCollection<string>();
        }

        public override string Header
        {
            get { return "Find"; }
        }

        protected override void OK()
        {
            QueryResults = Collection.FindAllAs<BsonDocument>();
        }

        public IEnumerable<BsonDocument> QueryResults
        {
            get { return _queryResults; }
            set
            {
                this.SetProperty(ref _queryResults, value);
            }
        }

        public ICommand AddFieldToSortCommand { get; private set; }

        private void AddFieldToSort()
        {
            
        }
        public ObservableCollection<string> FieldsToView { get; private set; }
        public ObservableCollection<string> FieldsToSort { get; private set; }
        public string FindCriteria { get; set; }
    }
}
