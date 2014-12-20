using System.Data;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.CSharp.RuntimeBinder;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerCollectionViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly MongoCollection<BsonDocument> _coll;
        public DatabaseExplorerCollectionViewModel(MongoCollection<BsonDocument> coll)
            : base(coll.Name, ItemType.Collection)
        {
            _coll = coll;
            FindCommand = new OpenTabCommand(() => new FindViewModel(_coll));
            StatsCommand = new OpenTabCommand(() => new CollectionStatsViewModel(_coll));
            AddDocumentCommand = new OpenTabCommand(() => new AddDocumentViewModel(_coll));
            AggregateCommand = new OpenTabCommand(() => new AggregateViewModel(_coll));
            AddIndexCommand = new OpenTabCommand(() => new AddIndexViewModel(_coll));
            MakeCappedCommand = new OpenTabCommand(() => new MakeCollectionCappedViewModel(_coll));
            RenameFieldsCommand = new OpenTabCommand(() => new RenameFieldsViewModel(_coll));
            MakeTTLCommand = new OpenTabCommand(() => new MakeTTLViewModel(_coll));
            CompactCommand = new NoWeakRelayCommand(() => _coll.Database.RunCommand(new CommandDocument("compact", _coll.Name)));
            DropCommand = new RelayCommand(() =>
            {
                MessengerInstance.Send(new NotificationMessage<DatabaseExplorerCollectionViewModel>(this, this,
                    "dropped"));
                _coll.Drop();
            });
        }
        [WindowCommand("ttl")]
        public ICommand MakeTTLCommand { get; set; }

        [WindowCommand("Переименовать поле")]
        public ICommand RenameFieldsCommand { get; set; }
        [WindowCommand("Сделать ограниченной")]
        public ICommand MakeCappedCommand { get; private set; }
        [WindowCommand("Запрос", IsDefault = true)]
        public ICommand FindCommand { get; private set; }
        [WindowCommand("Статистика")]
        public ICommand StatsCommand { get; private set; }
        [WindowCommand("Добавить документ")]
        public ICommand AddDocumentCommand { get; private set; }
        [WindowCommand("Добавить индекс")]
        public ICommand AddIndexCommand { get; private set; }
        [WindowCommand("Агрегация")]
        public ICommand AggregateCommand { get; private set; }
        [WindowCommand("Сжать")]
        public ICommand CompactCommand { get; private set; }
        [WindowCommand("Удалить")]
        public ICommand DropCommand { get; private set; }
    }
}