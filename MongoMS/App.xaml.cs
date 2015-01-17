using System.Windows;
using Microsoft.Practices.Prism;

namespace MongoMS
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new AppBootstrapper();
            bootstrapper.Run();
        }
    }
}
