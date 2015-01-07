using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.CreateDatabase.Addin.View;
using MenuCommand = MongoMS.Common.MenuCommand;

namespace MongoMS.CreateDatabase.Addin
{
    public class CreateDatabaseAddin:IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IRegionManager _regionManager;

        public CreateDatabaseAddin(IUnityContainer unity,IRegionManager regionManager)
        {
            _unity = unity;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Server.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "CreateDatabase";
            mc.Command = new DelegateCommand<MongoServer>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoServer mongoServer)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<MainView>());

        }
    }
}
