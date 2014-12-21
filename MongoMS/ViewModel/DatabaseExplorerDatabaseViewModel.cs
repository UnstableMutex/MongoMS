using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    internal class DatabaseExplorerDatabaseViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly MongoDatabase _db;

        public DatabaseExplorerDatabaseViewModel(MongoDatabase db)
            : base(db.Name, ItemType.Database)
        {
            _db = db;

            GetCollections();
            MessengerInstance.Register(this,
                (NotificationMessage<DatabaseExplorerCollectionViewModel> x) => AddedColl(x));
            AssignCommands<NoWeakRelayCommand>();
            JoinTablesFromMSSQLCommand = new OpenTabCommand(() => new JoinSQLTablesViewModel(db));
        }

        [WindowCommand("+Коллекция")]
        public ICommand AddCollectionCommand { get; private set; }

        [WindowCommand("Подробно")]
        public ICommand DBDetailsCommand { get; private set; }

        [WindowCommand("Экспорт из MSSQL")]
        public ICommand ExportFromMSSQLCommand { get; private set; }

        [WindowCommand("Объединить таблицы из MSSQL")]
        public ICommand JoinTablesFromMSSQLCommand { get; private set; }

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
            IEnumerable<string> colls = _db.GetCollectionNames();
            foreach (string coll in colls)
            {
                Children.Add(new DatabaseExplorerCollectionViewModel(_db.GetCollection<BsonDocument>(coll)));
            }
        }

        private void AddCollection()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddCollectionViewModel(_db));
        }

        private void DBDetails()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new DatabaseDetailsViewModel(_db));
        }

        private void Drop()
        {
            //SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add( new AddCollectionViewModel(_db));
        }

        private void ExportFromMSSQL()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new ExportMSSQLViewModel(_db));
        }
    }
}