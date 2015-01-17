using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.CreateDatabase.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly IUnityContainer _unity;
        private readonly MongoServer _server;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public MainViewModel(IUnityContainer unity, MongoServer server,IEventAggregator eventAggregator,IRegionManager regionManager)
        {
            _unity = unity;
            _server = server;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        public string DatabaseName { get; set; }
        protected override void OK()
        {

            var db = _server.GetDatabase(DatabaseName);
            db.GetStats();
            DatabaseAction a=new DatabaseAction();
            a.Action= ActionType.Create;
            a.DatabaseName = DatabaseName;
            a.Server = _server;
            _eventAggregator.GetEvent<PubSubEvent<DatabaseAction>>().Publish(a);
            CloseThisTab();

        }

        private void CloseThisTab()
        {
            RaiseCloseRequest();
           
        }
    }
}
