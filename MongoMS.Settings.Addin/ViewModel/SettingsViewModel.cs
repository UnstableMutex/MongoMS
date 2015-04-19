using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using MongoMS.Common;

namespace MongoMS.Settings.Addin.ViewModel
{
    class SettingsViewModel:BindableBase,ITabContent
    {
        public string Header { get { return "Settings"; } }
    }
}
