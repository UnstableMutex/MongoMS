using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.ViewModel
{
    internal class DatabaseExplorerServerViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly MongoServer _serv;


        public DatabaseExplorerServerViewModel(string name, MongoServer serv) : base(name, ItemType.Server)
        {
            _serv = serv;

            GetDatabases();
            AddDBCommand = new OpenTabCommand(() => new AddDatabaseViewModel(_serv));
            ServerOverviewCommand = new OpenTabCommand(() => new ServerOverViewViewModel(_serv));
            MessengerInstance.Register(this,
                (NotificationMessage<DatabaseExplorerDatabaseViewModel> x) => DBAddedMessage(x));
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

        private void DBAddedMessage(NotificationMessage<DatabaseExplorerDatabaseViewModel> message)
        {
            Children.Add(message.Content);
        }

        private void GetDatabases()
        {
            IEnumerable<string> dbs = (_serv.GetDatabaseNames());

            foreach (string db in dbs)
            {
                Children.Add(new DatabaseExplorerDatabaseViewModel(_serv.GetDatabase(db)));
            }
        }

        //void AddDB()
        //{
        //    SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddDatabaseViewModel(_serv));
        //}
    }
}