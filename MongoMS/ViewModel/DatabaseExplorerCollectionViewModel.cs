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

            AssignCommands<NoWeakRelayCommand>();
        }
        [WindowCommand("Запрос")]
        public ICommand FindCommand { get; private set; }

        void Find()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new FindViewModel(_coll));
        }
         [WindowCommand("Статистика")]
        public ICommand StatsCommand { get; private set; }

        void Stats()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new CollectionStatsViewModel(_coll));
        }
         [WindowCommand("Добавить документ")]
        public ICommand AddDocumentCommand { get; private set; }

        void AddDocument()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AddDocumentViewModel(_coll));
        }
         [WindowCommand("Агрегация")]
        public ICommand AggregateCommand { get; private set; }

        void Aggregate()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(new AggregateViewModel(_coll));
        }
    }
}