using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.ImportFromMSSQL.Addin.View;

namespace MongoMS.ImportFromMSSQL.Addin
{
    public class ImportFromSQLServerAddin:DatabaseCommandAddinBase<MainView>
    {
        public ImportFromSQLServerAddin(IRegionManager regionManager, IUnityContainer unity) : base(regionManager, unity)
        {
        }

        protected override string GetMenuItemName()
        {
            return "ImportfromSqlserver";
        }
    }
}
