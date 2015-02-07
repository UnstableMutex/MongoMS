using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.MakeCollectionCapped.Addin.View;

namespace MongoMS.MakeCollectionCapped.Addin
{
    public class MakeCollectionCappedAddin:CollectionCommandAddinBase<MainView>
    {
        public MakeCollectionCappedAddin(IUnityContainer unity, IRegionManager regionManager) : base(unity, regionManager)
        {
        }

        protected override string GetMenuItemName()
        {
            return "MakeCapped";
        }
    }
}
