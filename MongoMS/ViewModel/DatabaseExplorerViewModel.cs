using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerViewModel
    {
        public DatabaseExplorerViewModel()
        {
            Servers=new ObservableCollection<DatabaseExplorerServerViewModel>();
        }
        public ObservableCollection<DatabaseExplorerServerViewModel> Servers { get; private set; } 
    }
}
