using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using MongoMS.Common;
using MongoMS.Common.AddinBase;
using MongoMS.Find.Addin.View;

namespace MongoMS.Find.Addin
{
    public class FindAddin:CollectionCommandAddinBase<FindView>
    {
        protected override string GetMenuItemName()
        {
            return "Find...";
        }

        public FindAddin(IRegionManager regionManager, IUnityContainer unity):base(unity,regionManager){}

    }
}
