using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;

namespace MongoMS.Connect.Addin
{
    public class ConnectAddin : IModule
    {
        private readonly IRegionManager _regionManager;

        public ConnectAddin(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        Button GetConnectButton()
        {
            Button b = new Button();
            b.Content = "Connect";
            b.Click += b_Click;
            return b;
        }

        void b_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, GetConnectTab());
        }

        private TabItem GetConnectTab()
        {
            var ti = new TabItem();
            ti.Header = "sdfsdf";
            return ti;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion(RegionNames.TopMenuRegion, GetConnectButton());
        }
    }

}
