using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using MongoMS.Common;
using MongoMS.Connect.Addin.View;

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

        private UserControl GetConnectTab()
        {
            return new MainView();
        }

        void close_Click(object sender, RoutedEventArgs e)
        {
            var b = (sender as Button);
            _regionManager.Regions[RegionNames.TabControlRegion].Remove(b.Tag);
        }

        public void Initialize()
        {
            _regionManager.AddToRegion(RegionNames.TopMenuRegion, GetConnectButton());
        }
    }

}
