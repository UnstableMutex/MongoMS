using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class RenameFieldsViewModel:VMB
    {
        public RenameFieldsViewModel()
        {
            AssignCommands<NoWeakRelayCommand>();
        }
        public string OldName { get; set; }
        public string NewName { get; set; }
        public ICommand OKCommand { get; private set; }

        private void OK()
        {
            
        }
    }
}
