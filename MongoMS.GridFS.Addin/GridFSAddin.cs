using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.Events;
using MongoMS.GridFS.Addin.View;


namespace MongoMS.GridFS.Addin
{
    public class GridFSAddin:IModule
    {
         private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public GridFSAddin(IUnityContainer unity, IEventAggregator eventAggregator,IRegionManager regionManager)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
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
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<GridFSView>(new ParameterOverride("database", obj)));
        }
    }
}
