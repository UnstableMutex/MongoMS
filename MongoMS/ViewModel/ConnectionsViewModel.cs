using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.ViewModel
{
    class ConnectionsViewModel
    {
        public ObservableDictionary<string,string> Connections { get; private set; } 
    }
}
