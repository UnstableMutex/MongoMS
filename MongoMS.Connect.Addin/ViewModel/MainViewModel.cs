using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.Connect.Addin.ViewModel
{
    public class MainViewModel : OKViewModel
    {
        private readonly IUnityContainer _unity;

        public MainViewModel(IUnityContainer unity)
        {
            _unity = unity;
            Connections = new ObservableCollection<ConnectionViewModel>();
            AddNewCommand = new DelegateCommand(AddNew);
        }
        private ObservableCollection<ConnectionViewModel> _connections;

        public ObservableCollection<ConnectionViewModel> Connections
        {
            get { return _connections; }
            private set { _connections = value; }
        }
        private ConnectionViewModel _selected;

        public ConnectionViewModel Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }
        public ICommand AddNewCommand { get; private set; }

        private void AddNew()
        {


            var conn = _unity.Resolve<ConnectionViewModel>();
            conn.Name = "local";
            conn.CS.Server =new MongoServerAddress("localhost"); 
            Connections.Add(conn);
            Selected = conn;

        }

        public override string Header
        {
            get { return "Connect"; }
        }

        public override void BeforeClose()
        {
            MessageBox.Show("dsfsdf");
        }
    }
}
