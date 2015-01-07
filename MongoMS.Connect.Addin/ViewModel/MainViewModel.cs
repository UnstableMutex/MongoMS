using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace MongoMS.Connect.Addin.ViewModel
{
    class MainViewModel:BindableBase
    {
        public MainViewModel()
        {
            Connections = new ObservableCollection<ConnectionViewModel>();
        }
        private ObservableCollection<ConnectionViewModel> _connections;

        public ObservableCollection<ConnectionViewModel> Connections
        {
            get { return _connections; }
           private set { _connections = value; }
        }
    }
}
