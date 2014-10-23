using System.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerDatabaseViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly MongoDatabase _db;


        public DatabaseExplorerDatabaseViewModel(MongoDatabase db)
            : base(db.Name, ItemType.Database)
        {
            _db = db;

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
            var colls = _db.GetCollectionNames();
            foreach (var coll in colls)
            {
                Children.Add(new DatabaseExplorerCollectionViewModel(_db.GetCollection<BsonDocument>(coll)));
            }
        }
        public ICommand AddCollectionCommand { get; private set; }

        void AddCollection()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddCollectionViewModel(_db));
        }
        public ICommand DropCommand { get; private set; }

        void Drop()
        {
            //SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add( new AddCollectionViewModel(_db));
        }
        public ICommand ExportFromMSSQLCommand { get; private set; }

        void ExportFromMSSQL()
        {
             SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add( new ExportMSSQLViewModel(_db));
        }
    }
}