using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;


namespace MongoMS.DropDatabase.Addin
{
    public class DropDatabaseAddin : IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;

        public DropDatabaseAddin(IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "DropDatabase";
            mc.Command = new DelegateCommand<MongoDatabase>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoDatabase obj)
        {
            DatabaseAction dba = new DatabaseAction();
            dba.Action = ActionType.Drop;
            dba.DatabaseName = obj.Name;
            dba.Server = obj.Server;
            obj.Drop();
            _eventAggregator.GetEvent<PubSubEvent<DatabaseAction>>().Publish(dba);
        }
    }
}
