using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;

namespace MongoMS.Common.AddinBase
{
   
      public abstract class CollectionCommandAddinBase<T>:IModule where T:UserControl
    {
        private readonly IUnityContainer _unity;
        private readonly IRegionManager _regionManager;

        public CollectionCommandAddinBase(IUnityContainer unity, IRegionManager regionManager)
        {
            _unity = unity;
            _regionManager = regionManager;
        }

          protected abstract string GetMenuItemName();


        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = GetMenuItemName();
            mc.Command = new DelegateCommand<MongoCollection>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoCollection obj)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<T>(new ParameterOverride("collection", obj)));

        }
    }
  
}
