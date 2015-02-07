using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using MongoMS.AddDocument.Addin.View;
using MongoMS.Common.AddinBase;


namespace MongoMS.AddDocument.Addin
{
    public class AddDocumentAddin:CollectionCommandAddinBase<AddDocumentView>
    {
        protected override string GetMenuItemName()
        {
            return "Add Doc...";
        }

        public AddDocumentAddin(IRegionManager regionManager, IUnityContainer unity):base(unity,regionManager)
        {
           
        }

      
    }
}
