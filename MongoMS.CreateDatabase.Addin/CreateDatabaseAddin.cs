using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using MongoMS.Common;
using MenuCommand = MongoMS.Common.MenuCommand;

namespace MongoMS.CreateDatabase.Addin
{
    public class CreateDatabaseAddin:IModule
    {
        private readonly IUnityContainer _unity;

        public CreateDatabaseAddin(IUnityContainer unity)
        {
            _unity = unity;
        }

        public void Initialize()
        {
            var servermenu = _unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.Server.ToString());
            MenuCommand mc=new MenuCommand("CreateDatabase");
            servermenu.Add(mc);
        }
    }
}
