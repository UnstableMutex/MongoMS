using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class ServerViewModel
    {
        private readonly IUnityContainer _unity;
      
        private readonly string _name;
        private readonly string _cs;


        public ServerViewModel(string name, string cs)
        {
            _name = name;
            _cs = cs;
         var   eventAggregator = UnityHolder.Unity.Resolve<IEventAggregator>();
            InitChildren();
            eventAggregator.GetEvent<PubSubEvent<DatabaseAction>>().Subscribe(OnDbListChanged);
        }

        private void OnDbListChanged(DatabaseAction databaseAction)
        {
            MongoServer s = new MongoClient(_cs).GetServer();
            if (databaseAction.Action == ActionType.Create)
            {

                if (databaseAction.Server == s)
                {
                    Children.Add(new DatabaseViewModel(s.GetDatabase(databaseAction.DatabaseName)));
                }
                return;
            }
            if (databaseAction.Action == ActionType.Drop)
            {
                if (databaseAction.Server == s)
                {
                    var db = Children.SingleOrDefault(x => x.Name == databaseAction.DatabaseName);
                    if (db != null)
                    {
                        Children.Remove(db);
                    }
                }
            }

        }


        private void InitChildren()
        {
            Children = new ObservableCollection<DatabaseViewModel>();
            MongoServer s = new MongoClient(_cs).GetServer();
       
            foreach (var dbname in s.GetDatabaseNames())
            {
                Children.Add(new DatabaseViewModel(s.GetDatabase(dbname)));
            }
        }

        public ObservableCollection<DatabaseViewModel> Children { get; private set; }

        public string Name
        {
            get { return _name; }
        }

        public object CmdParameter
        {
            get
            {
                return new MongoClient(_cs).GetServer();
            }
        }

        public ObservableCollection<IMenuCommand> Menu
        {
            get
            {
                var res = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Server.ToString());
                return res;
            }
        }
    }
}