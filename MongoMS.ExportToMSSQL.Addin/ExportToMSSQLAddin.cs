using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.ExportToMSSQL.Addin.View;

namespace MongoMS.ExportToMSSQL.Addin
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
