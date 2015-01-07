using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
namespace MongoMS
{
    class AppBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            const string addinsPath = "Addins";
            if (Directory.Exists(addinsPath))
            {
                DirectoryModuleCatalog mc = new DirectoryModuleCatalog();
                mc.ModulePath = addinsPath;
                return mc;
            }
            else
            {
                return new ModuleCatalog();
            }
        }
    }
}
