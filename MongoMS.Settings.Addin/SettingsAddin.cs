using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common;
using MongoMS.Settings.Addin.View;

namespace MongoMS.Settings.Addin
{
    public class SettingsAddin : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unity;

        public SettingsAddin(IRegionManager regionManager, IUnityContainer unity)
        {
            _regionManager = regionManager;
            _unity = unity;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion(RegionNames.TopMenuRegion, GetSettingsButton());
        }

        private object GetSettingsButton()
        {
            var b = new Button {Content = "Settings"};
            b.Click += b_Click;
            return b;
        }


        void b_Click(object sender, RoutedEventArgs e)
        {
            _regionManager.AddToRegion(RegionNames.TabControlRegion, GetSettingsTab());
        }
        private UserControl GetSettingsTab()
        {
            return _unity.Resolve<SettingsView>();
        }
    }
}
