using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class DatabaseViewModel
    {
        private readonly MongoDatabase _db;
     

        public DatabaseViewModel(MongoDatabase db)
        {
            _db = db;

         var   eventAggregator = UnityHolder.Unity.Resolve<IEventAggregator>();
            InitChildren();
            eventAggregator.GetEvent<PubSubEvent<CollectionAction>>().Subscribe(CollectionListChanged);

        }

        private void CollectionListChanged(CollectionAction collectionAction)
        {
            
            if (collectionAction.Action == ActionType.Create)
            {

                if (collectionAction.Database == _db)
                {
                    Children.Add(new CollectionViewModel(_db.GetCollection(collectionAction.CollectionName)));
                }
                return;
            }
            if (collectionAction.Action == ActionType.Drop)
            {
                if (collectionAction.Database == _db)
                {
                    var db = Children.SingleOrDefault(x => x.Name == collectionAction.CollectionName);
                    if (db != null)
                    {
                        Children.Remove(db);
                    }
                }
            }

        }

        public string Name
        {
            get { return _db.Name; }
        }
        public object CmdParameter
        {
            get { return _db; }
        }
        public ObservableCollection<IMenuCommand> Menu
        {
            get
            {
                return UnityHolder.Unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            }
        }
        private void InitChildren()
        {
            Children = new ObservableCollection<CollectionViewModel>();
            foreach (var dbname in _db.GetCollectionNames())
            {
                Children.Add(new CollectionViewModel(_db.GetCollection(dbname)));

            }
        }

        public ObservableCollection<CollectionViewModel> Children { get; private set; }
    }
}