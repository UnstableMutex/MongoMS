using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class DatabaseViewModel
    {
        private readonly MongoDatabase _db;
        private readonly IUnityContainer _unity;


        public DatabaseViewModel(MongoDatabase db, IUnityContainer unity)
        {
            _db = db;
            _unity = unity;
            InitChildren();
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
                return _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            }
        }
        private void InitChildren()
        {
            Children = new ObservableCollection<CollectionViewModel>();
           
            foreach (var dbname in _db.GetCollectionNames())
            {
                Children.Add(new CollectionViewModel(_db.GetCollection(dbname), _unity));
            }
        }

        public ObservableCollection<CollectionViewModel> Children { get; private set; }
    }
}