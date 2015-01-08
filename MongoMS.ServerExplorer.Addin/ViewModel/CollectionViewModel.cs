using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class CollectionViewModel
    {
        private readonly MongoCollection _collection;
        private readonly IUnityContainer _unity;

        public CollectionViewModel(MongoCollection collection, IUnityContainer unity)
        {
            _collection = collection;
            _unity = unity;
        }

        public object CmdParameter
        {
            get { return _collection; }
        }
        public string Name { get { return _collection.Name; } }
        public ObservableCollection<IMenuCommand> Menu
        {
            get
            {
                var res= _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
                return res;
            }
        }
    }
}