using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExportToMSSQL.View;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;

namespace ExportToMSSQL
{
    public class ExportToMSSQLAddin : CollectionCommandAddinBase<MainView>
    {
        public ExportToMSSQLAddin(IUnityContainer unity, IRegionManager regionManager) : base(unity, regionManager)
        {
        }

        protected override string GetMenuItemName()
        {
            return "MSSQLExport";
        }
    }
}
