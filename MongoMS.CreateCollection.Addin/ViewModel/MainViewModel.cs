using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;
namespace MongoMS.CreateCollection.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoDatabase _database;
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        public MainViewModel(MongoDatabase database, IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _database = database;
            _unity = unity;
            _eventAggregator = eventAggregator;
        }
        public string CollectionName { get; set; }
        protected override void OK()
        {
            var coll = _database.CreateCollection(CollectionName);
            CollectionAction a = new CollectionAction();
            a.Action = ActionType.Create;
            a.CollectionName =CollectionName;
            a.Database = _database;
            _eventAggregator.GetEvent<PubSubEvent<CollectionAction>>().Publish(a);
            RaiseCloseRequest();
        }

        public override string Header
        {
            get { return "New Collection"; }
        }
    }
}
