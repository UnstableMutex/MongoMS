using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Aggregate.Addin.View;
using MongoMS.Common.AddinBase;

namespace MongoMS.Aggregate.Addin
{
    public class AggregateAddin:CollectionCommandAddinBase<MainView>
    {
        protected override string GetMenuItemName()
        {
            return "Aggregate...";
        }

      
        public AggregateAddin(IRegionManager regionManager, IUnityContainer unity):base(unity,regionManager)
        {
           
        }

    }
}
