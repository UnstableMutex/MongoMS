using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace MongoMS.ServerExplorer.Addin
{
    public class ServerExplorerAddin:IModule
    {
        private readonly IUnityContainer _unity;
        private readonly IEventAggregator _eventAggregator;

        public void Initialize()
        {
          
        }

        public ServerExplorerAddin(IUnityContainer unity, IEventAggregator eventAggregator)
        {
            _unity = unity;
            _eventAggregator = eventAggregator;
        }
    }
}
