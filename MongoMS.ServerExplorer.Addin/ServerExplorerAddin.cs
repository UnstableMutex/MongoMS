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
using MongoMS.Common.Events;
using MongoMS.ServerExplorer.Addin.View;
using MongoMS.ServerExplorer.Addin.ViewModel;

namespace MongoMS.ServerExplorer.Addin
{
    public class ServerExplorerAddin:IModule
    {
       
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private ServerExplorerViewModel _viewModel;
        public void Initialize()
        {
             _viewModel = UnityHolder.Unity.Resolve<ServerExplorerViewModel>();
            var v = new ServerExplorerView(_viewModel);
            _regionManager.RegisterViewWithRegion(RegionNames.ServerExplorerRegion,()=>v);
        }

        public ServerExplorerAddin(IUnityContainer unity, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            UnityHolder.Unity = unity;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            var e = _eventAggregator.GetEvent<PubSubEvent<RequestConnect>>();
            e.Subscribe(OnConnectRequest, true);
        }

        void OnConnectRequest(RequestConnect rc)
        {
           _viewModel.Servers.Add(new ServerViewModel(rc.Name, rc.ConnectionString));
        }
    }
}
