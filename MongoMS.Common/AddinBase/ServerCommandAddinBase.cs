using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;

namespace MongoMS.Common.AddinBase
{
   public abstract class ServerCommandAddinBase<T>:IModule
    {
         private readonly IUnityContainer _unity;
        private readonly IRegionManager _regionManager;

        public ServerCommandAddinBase(IUnityContainer unity, IRegionManager regionManager)
        {
            _unity = unity;
            _regionManager = regionManager;
        }
        protected abstract string GetMenuItemName();
        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Server.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = GetMenuItemName();
            mc.Command = new DelegateCommand<MongoServer>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoServer mongoServer)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<T>(new ParameterOverride("server",mongoServer)));

        }
    }
}
