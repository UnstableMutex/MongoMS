using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Добавить БД")]
    [CommandLevel(Level.Server)]
    internal class AddDatabaseViewModel : OKVMB
    {
        private readonly MongoServer _serv;

        public AddDatabaseViewModel(MongoServer serv)
        {
            _serv = serv;
            AssignCommands<NoWeakRelayCommand>();
        }

        public string Name { get; set; }

        protected override void OK()
        {
            var db = new DatabaseExplorerDatabaseViewModel(_serv.GetDatabase(Name));
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerDatabaseViewModel>(this, db, "added"));
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
        }
    }
}