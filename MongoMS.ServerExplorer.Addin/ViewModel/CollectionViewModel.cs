using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class CollectionViewModel:IDatabaseChild
    {
        private readonly MongoCollection _collection;


        public CollectionViewModel(MongoCollection collection)
        {
            _collection = collection;

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
                var res = UnityHolder.Unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
                return res;
            }
        }
    }
}