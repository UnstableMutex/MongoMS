using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace MongoMS.Common
{
   public class OKViewModel:BindableBase,ICloseRequest,ITabContent
    {
        public OKViewModel()
        {
          OKCommand = new DelegateCommand(OK,CanOK);
        }

        protected virtual bool CanOK()
        {
            return true;
        }

        public ICommand OKCommand { get; private set; }

        protected virtual void OK()
        {
           
        }


       public virtual void BeforeClose()
       {
          
       }

       public event EventHandler CloseRequest;

       protected void RaiseCloseRequest()
       {
           if (CloseRequest != null)
           {
               CloseRequest(this, EventArgs.Empty);
           }
       }

       public virtual string Header { get { return "Header"; } }
    }
}
