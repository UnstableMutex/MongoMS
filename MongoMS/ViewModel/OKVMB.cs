using System.Windows.Input;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class OKVMB : VMB
    {
       
        public OKVMB()
        {
          
        }
     
        public ICommand OKCommand { get; protected set; }

        protected virtual void OK()
        {

        }

        protected virtual bool CanOK()
        {
            return true;
        }
    }
}