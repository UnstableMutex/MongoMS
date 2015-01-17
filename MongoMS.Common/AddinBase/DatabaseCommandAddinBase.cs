using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;

namespace MongoMS.Common.AddinBase
{
    public abstract class DatabaseCommandAddinBase<T> : IModule where T : UserControl
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unity;

        public DatabaseCommandAddinBase(IRegionManager regionManager, IUnityContainer unity)
        {
            _regionManager = regionManager;
            _unity = unity;
        }
        protected abstract string GetMenuItemName();

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Database.ToString());
            MenuCommand mc = _unity.Resolve<MenuCommand>();
            mc.Name = GetMenuItemName();
            mc.Command = new DelegateCommand<MongoDatabase>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoDatabase obj)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, _unity.Resolve<T>(new ParameterOverride("database", obj)));
        }
    }
}
