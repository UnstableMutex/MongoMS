using System.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;
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
            JoinTablesFromMSSQLCommand = new OpenTabCommand(() => new JoinSQLTablesViewModel(db));
        }

        private void AddedColl(NotificationMessage<DatabaseExplorerCollectionViewModel> notificationMessage)
        {
            if (notificationMessage.Notification == "added")
            {
                Children.Add(notificationMessage.Content);
            }
            else
            {
                Children.Remove(notificationMessage.Content);
            }
        }

        private void GetCollections()
        {
            var colls = _db.GetCollectionNames();
            foreach (var coll in colls)
            {
                Children.Add(new DatabaseExplorerCollectionViewModel(_db.GetCollection<BsonDocument>(coll)));
            }
        }
        [WindowCommand("+Коллекция")]
        public ICommand AddCollectionCommand { get; private set; }

        void AddCollection()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddCollectionViewModel(_db));
        }
        [WindowCommand("Подробно")]
        public ICommand DBDetailsCommand { get; private set; }

        void DBDetails()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new DatabaseDetailsViewModel(_db));
        }
        [WindowCommand("-коллекция")]
        public ICommand DropCommand { get; private set; }

        void Drop()
        {
            //SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add( new AddCollectionViewModel(_db));
        }
        [WindowCommand("Экспорт из MSSQL")]
        public ICommand ExportFromMSSQLCommand { get; private set; }

        void ExportFromMSSQL()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new ExportMSSQLViewModel(_db));
        }
        [WindowCommand("Объединить таблицы из MSSQL")]
        public ICommand JoinTablesFromMSSQLCommand { get; private set; }
    }
}