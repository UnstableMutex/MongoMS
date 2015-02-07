using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.TTL.Addin.View;

namespace MongoMS.TTL.Addin
{
    public class TTLAddin:CollectionCommandAddinBase<MainView>
    {
        protected override string GetMenuItemName()
        {
            return "TTL...";
        }


        public TTLAddin(IRegionManager regionManager, IUnityContainer unity):base(unity,regionManager)
        {
            
        }

       
    }
}
