using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;

namespace MongoMS.CreateCollection.Addin
{
    public class CreateCollectionAddin:IModule
    {
        private readonly IRegionManager _regionManager;

        public CreateCollectionAddin(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        Button GetMenuButton()
        {
            Button b=new Button();
            b.Content = "Click me";
            return b;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion(RegionNames.TopMenuRegion, GetMenuButton());
        }
    }
}
