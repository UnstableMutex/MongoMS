using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerDatabaseViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly string _cs;

        public DatabaseExplorerDatabaseViewModel(string name, string cs)
            : base(name, ItemType.Database)
        {
            _cs = cs;
            GetCollections();
            MessengerInstance.Register(this, (NotificationMessage<DatabaseExplorerCollectionViewModel> x) => AddedColl(x));
            AssignCommands<NoWeakRelayCommand>();
        }

        private void AddedColl(NotificationMessage<DatabaseExplorerCollectionViewModel> notificationMessage)
        {
            Children.Add(notificationMessage.Content);



        }

        private void GetCollections()
        {
            var colls = new MongoClient(_cs).GetServer().GetDatabase(Name).GetCollectionNames();
            foreach (var coll in colls)
            {
                Children.Add(new DatabaseExplorerCollectionViewModel(coll, _cs, Name));
            }
        }
        public ICommand AddCollectionCommand { get; private set; }

        void AddCollection()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content = new AddCollectionViewModel(_cs, Name);
        }
    }
}