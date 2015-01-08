using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class ServerExplorerViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ServerExplorerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Servers = new ObservableCollection<ServerViewModel>();
        }
        public ObservableCollection<ServerViewModel> Servers { get; private set; }
    }

    public class ServerViewModel
    {
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        private readonly string _name;
        private readonly string _cs;

        ServerViewModel(string name, string cs)
        {
            _name = name;
            _cs = cs;
            InitChildren();
        }

        public ServerViewModel(string name, string connectionString, IUnityContainer unity,IEventAggregator eventAggregator)
            : this(name, connectionString)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<PubSubEvent<DatabaseAction>>().Subscribe(OnDbListChanged);
        }

        private void OnDbListChanged(DatabaseAction databaseAction)
        {
            if (databaseAction.Action == ActionType.Create)
            {
                MongoServer s = new MongoClient(_cs).GetServer();
                if (databaseAction.Server == s)
                {
                      Children.Add(new DatabaseViewModel(s.GetDatabase(databaseAction.DatabaseName)));
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
                return _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Server.ToString());
            }
        }
    }
    public class DatabaseViewModel
    {
        private readonly MongoDatabase _db;


        public DatabaseViewModel(MongoDatabase db)
        {
            _db = db;
        }

        public string Name
        {
            get { return _db.Name; }
        }
    }
}
