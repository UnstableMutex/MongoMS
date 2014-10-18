using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerServerViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly string _cs;

        public DatabaseExplorerServerViewModel(string name,string cs):base(name, ItemType.Server)
        {
            _cs = cs;
            GetDatabases();
            AssignCommands<NoWeakRelayCommand>();
            MessengerInstance.Register(this, (NotificationMessage<DatabaseExplorerDatabaseViewModel> x) => DBAddedMessage(x));
        }

        void DBAddedMessage(NotificationMessage<DatabaseExplorerDatabaseViewModel> message)
        {
            Children.Add(message.Content);
        }

        private void GetDatabases()
        {
            var dbs = (new MongoClient(_cs).GetServer().GetDatabaseNames());
            foreach (var db in dbs)
            {
                    Children.Add(new DatabaseExplorerDatabaseViewModel(db,_cs));

            }
        }
        public ICommand AddDBCommand { get; private set; }

        void AddDB()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content = new AddDatabaseViewModel(_cs);
        }
    }
}