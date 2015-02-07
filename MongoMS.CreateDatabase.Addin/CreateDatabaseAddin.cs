using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.Common.AddinBase;
using MongoMS.CreateDatabase.Addin.View;

namespace MongoMS.CreateDatabase.Addin
{
    public class CreateDatabaseAddin:ServerCommandAddinBase<MainView>
    {
        protected override string GetMenuItemName()
        {
            return "CreateDatabase";
        }

        public CreateDatabaseAddin(IUnityContainer unity,IRegionManager regionManager):base(unity,regionManager)
        {

        }

     
    }
}
