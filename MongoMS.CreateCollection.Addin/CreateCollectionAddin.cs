using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.CreateCollection.Addin.View;

namespace MongoMS.CreateCollection.Addin
{
    public class CreateCollectionAddin:IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unity;

        public CreateCollectionAddin(IRegionManager regionManager,IUnityContainer unity)
        {
            _regionManager = regionManager;
            _unity = unity;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = "CreateCollection";
            mc.Command = new DelegateCommand<MongoDatabase>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoDatabase obj)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<MainView>(new ParameterOverride("database", obj)));
        }
    }
}
