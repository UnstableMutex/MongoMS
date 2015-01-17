using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.MapReduce.Addin.View;

namespace MongoMS.MapReduce.Addin
{
    public class MapReduceAddin:CollectionCommandAddinBase<MainView>
    {
        public MapReduceAddin(IUnityContainer unity, IRegionManager regionManager) : base(unity, regionManager)
        {
        }

        protected override string GetMenuItemName()
        {
            return "MapReduce";
        }
    }
}
