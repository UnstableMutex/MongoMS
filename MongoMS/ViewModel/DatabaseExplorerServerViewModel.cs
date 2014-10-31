using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MongoMS.Common;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerServerViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly MongoServer _serv;


        public DatabaseExplorerServerViewModel(string name,MongoServer serv):base(name, ItemType.Server)
        {
            _serv = serv;

            GetDatabases();
            AddDBCommand = new OpenTabCommand(()=> new AddDatabaseViewModel(_serv));
            ServerOverviewCommand = new OpenTabCommand(()=>new ServerOverViewViewModel(_serv));
            MessengerInstance.Register(this, (NotificationMessage<DatabaseExplorerDatabaseViewModel> x) => DBAddedMessage(x));
        }

        void DBAddedMessage(NotificationMessage<DatabaseExplorerDatabaseViewModel> message)
        {
            Children.Add(message.Content);
        }

        private void GetDatabases()
        {
          
            var dbs = (_serv.GetDatabaseNames());

            foreach (var db in dbs)
            {
                    Children.Add(new DatabaseExplorerDatabaseViewModel(_serv.GetDatabase(db)));

            }
        }
        [WindowCommand("Отключиться")]
        public ICommand DisconnectCommand { get; private set; }

        //private void Disconnect()
        //{
           
        //}
        [WindowCommand("Overview")]
        public ICommand ServerOverviewCommand { get; private set; }


        [WindowCommand("Добавить БД")]
        public ICommand AddDBCommand { get; private set; }

        //void AddDB()
        //{
        //    SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddDatabaseViewModel(_serv));
        //}
    }
}