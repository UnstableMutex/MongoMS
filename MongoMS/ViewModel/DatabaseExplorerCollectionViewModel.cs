using System.Data;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
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
            CompactCommand = new NoWeakRelayCommand(() => _coll.Database.RunCommand(new CommandDocument("compact", _coll.Name)));
        }
        [WindowCommand("������������� ����")]
        public ICommand RenameFieldsCommand { get; set; }
        [WindowCommand("������� ������������")]
        public ICommand MakeCappedCommand { get; private set; }
        [WindowCommand("������", IsDefault = true)]
        public ICommand FindCommand { get; private set; }
        [WindowCommand("����������")]
        public ICommand StatsCommand { get; private set; }
        [WindowCommand("�������� ��������")]
        public ICommand AddDocumentCommand { get; private set; }
        [WindowCommand("�������� ������")]
        public ICommand AddIndexCommand { get; private set; }
        [WindowCommand("���������")]
        public ICommand AggregateCommand { get; private set; }
        [WindowCommand("�����")]
        public ICommand CompactCommand { get; private set; }

    }
}