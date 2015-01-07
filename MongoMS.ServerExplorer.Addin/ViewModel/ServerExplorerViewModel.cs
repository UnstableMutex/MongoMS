using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using MongoDB.Driver;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class ServerExplorerViewModel:BindableBase
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
        private readonly string _name;
        private readonly string _cs;

        public ServerViewModel(string name,string cs)
        {
            _name = name;
            _cs = cs;
            InitChildren();
        }

        private void InitChildren()
        {
                        Children = new ObservableCollection<DatabaseViewModel>();
            MongoServer s = new MongoClient(_cs).GetServer();
            foreach (var dbname in s.GetDatabaseNames())
            {
                Children.Add( new DatabaseViewModel(s.GetDatabase(dbname)));
            }

        }

        public ObservableCollection<DatabaseViewModel> Children { get; private set; } 

        public string Name
        {
            get { return _name; }
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
