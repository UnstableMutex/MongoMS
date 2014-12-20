using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    internal class OKVMB : VMB
    {
        public ICommand OKCommand { get; protected set; }

        protected virtual void OK()
        {
        }

        protected virtual bool CanOK()
        {
            return true;
        }

        protected void CloseThisTab()
        {
            //TODO move method to base class - its usable
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
        }
    }
}