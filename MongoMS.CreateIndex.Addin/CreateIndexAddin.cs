using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.CreateIndex.Addin.View;

namespace MongoMS.CreateIndex.Addin
{
    public class CreateIndexAddin:IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IRegionManager _regionManager;

        public CreateIndexAddin(IUnityContainer unity, IRegionManager regionManager)
        {
            _unity = unity;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "Indexes";
            mc.Command = new DelegateCommand<MongoCollection>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoCollection obj)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<MainView>(new ParameterOverride("collection", obj)));

        }
    }
}
