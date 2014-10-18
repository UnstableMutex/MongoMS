using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using MongoMS.ViewModel;

namespace MongoMS
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel mwm;
        protected override void OnStartup(StartupEventArgs e)
        {
            MVVMTemplateSelection.TypeSource.AddAssembly(Assembly.GetExecutingAssembly());
            base.OnStartup(e);
           SimpleIoc.Default.Register<MainViewModel>();
            mwm = SimpleIoc.Default.GetInstance<MainViewModel>();
            var mw = new MainWindow();
            mw.DataContext = mwm;
            mw.Show();


        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            mwm.Save();
        }
    }
}
