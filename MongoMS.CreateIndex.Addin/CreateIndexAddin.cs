using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
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
