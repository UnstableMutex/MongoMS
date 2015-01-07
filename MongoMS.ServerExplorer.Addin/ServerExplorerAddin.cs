using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common;
using MongoMS.ServerExplorer.Addin.View;

namespace MongoMS.ServerExplorer.Addin
{
    public class ServerExplorerAddin:IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ServerExplorerRegion, typeof(ServerExplorerView));
        }

        public ServerExplorerAddin(IUnityContainer unity, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            var e = _eventAggregator.GetEvent<PubSubEvent<RequestConnect>>();
            e.Subscribe(OnConnectRequest);
        }

        void OnConnectRequest(RequestConnect rc)
        {
          
            //_regionManager.AddToRegion(RegionNames.ServerExplorerRegion,)
        }
    }
}
