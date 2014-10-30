using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Добавить БД")]
    class AddDatabaseViewModel:VMB
    {
        private readonly MongoServer _serv;


        public AddDatabaseViewModel(MongoServer serv)
        {
            _serv = serv;

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


           
           DatabaseExplorerDatabaseViewModel db=new DatabaseExplorerDatabaseViewModel( _serv.GetDatabase(Name));
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerDatabaseViewModel>(this, db, "added"));
           SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);

        }
    }
}
