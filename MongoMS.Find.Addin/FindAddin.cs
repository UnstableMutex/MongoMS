using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
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
