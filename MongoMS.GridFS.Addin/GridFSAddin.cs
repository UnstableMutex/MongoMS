using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;


namespace MongoMS.GridFS.Addin
{
    public class GridFSAddin:IModule
    {
         private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;

        public GridFSAddin(IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "GridFS";
            mc.Command = new DelegateCommand<MongoDatabase>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoDatabase obj)
        {
           
        }
    }
}
