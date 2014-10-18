using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class AddCollectionViewModel:VMB
    {
        private readonly string _cs;
        private readonly string _db;

        public AddCollectionViewModel(string cs,string db)
        {
            _cs = cs;
            _db = db;
            AssignCommands<NoWeakRelayCommand>();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public ICommand OKCommand { get; private set; }

        void OK()
        {
            MessageBox.Show("OK");

            DatabaseExplorerCollectionViewModel coll=new DatabaseExplorerCollectionViewModel(Name,_cs,_db);
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerCollectionViewModel>(this,coll,"added"));


            SimpleIoc.Default.GetInstance<MainViewModel>().Content = null;
        }
    }
}
