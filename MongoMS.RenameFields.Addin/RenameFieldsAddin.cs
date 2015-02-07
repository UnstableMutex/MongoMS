using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.RenameFields.Addin.View;

namespace MongoMS.RenameFields.Addin
{
    public class RenameFieldsAddin:CollectionCommandAddinBase<MainView>
    {
        public RenameFieldsAddin(IUnityContainer unity, IRegionManager regionManager) : base(unity, regionManager)
        {
        }

        protected override string GetMenuItemName()
        {
            return "RenameFields";
        }
    }
}
