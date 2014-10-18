using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class AddDatabaseViewModel:VMB
    {
        private readonly string _cs;

        public AddDatabaseViewModel(string cs)
        {
            _cs = cs;
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
    
           DatabaseExplorerDatabaseViewModel db=new DatabaseExplorerDatabaseViewModel(Name,_cs);
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerDatabaseViewModel>(this, db, "added"));
        }
    }
}
