using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.AddinBase;
using MongoMS.CreateIndex.Addin.View;

namespace MongoMS.CreateIndex.Addin
{
    public class CreateIndexAddin:CollectionCommandAddinBase<MainView>
    {
    
        public CreateIndexAddin(IUnityContainer unity, IRegionManager regionManager):base(unity,regionManager)
        {
          
        }

        protected override string GetMenuItemName()
        {
            return "CreateIndex";
        }
    }
}
