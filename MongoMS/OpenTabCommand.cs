using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MongoMS.ViewModel;
using MVVMLight.Extras;

namespace MongoMS
{
    internal class OpenTabCommand : ICommand
    {
        private readonly Func<VMB> _vmFactory;

        public OpenTabCommand(Func<VMB> vmFactory)
        {
            _vmFactory = vmFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Add(_vmFactory());
        }

        public event EventHandler CanExecuteChanged;
    }
}