using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class ServerExplorerViewModel:BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _unity;
        private readonly IModuleCatalog _mc;
        private readonly IModuleManager _moduleManager;

        public ServerExplorerViewModel(IEventAggregator eventAggregator, IUnityContainer unity)
        {
            _eventAggregator = eventAggregator;
            _unity = unity;
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
        }

        public string Name
        {
            get { return _name; }
        }

    }

    public class DatabaseViewModel
    {
        public DatabaseViewModel(MongoDatabase db)
        {
            
        }
    }
}
