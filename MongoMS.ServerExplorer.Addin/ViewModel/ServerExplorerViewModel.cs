using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

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
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
