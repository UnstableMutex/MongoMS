using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.TTL.Addin
{
    public class TTLAddin:IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unity;

        public TTLAddin(IRegionManager regionManager, IUnityContainer unity)
        {
            _regionManager = regionManager;
            _unity = unity;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Collection.ToString());
            var mc = _unity.Resolve<MenuCommand>();
            mc.Name = "TTL...";
            mc.Command = new DelegateCommand<MongoCollection>(ExecuteMethod);
            servermenu.Add(mc);
        }

        private void ExecuteMethod(MongoCollection obj)
        {
            
        }
    }
}
