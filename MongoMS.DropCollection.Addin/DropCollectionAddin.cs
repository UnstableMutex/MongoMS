using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;

namespace MongoMS.DropCollection.Addin
{
    public class DropCollectionAddin:IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;

        public DropCollectionAddin(IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "DropCollection";
            mc.Command = new DelegateCommand<MongoCollection>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoCollection obj)
        {
            var dba = new CollectionAction();
            dba.Action = ActionType.Drop;
            dba.CollectionName = obj.Name;
            dba.Database = obj.Database;
            obj.Drop();
            _eventAggregator.GetEvent<PubSubEvent<CollectionAction>>().Publish(dba);
        }
    }
}
