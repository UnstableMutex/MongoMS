using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.CreateIndex.Addin.ViewModel
{
    public class MainViewModel:CollectionOKViewModel
    {
      
        public MainViewModel(MongoCollection collection):base(collection)
        {
           
        }

        public override string Header
        {
            get { return "New Index"; }
        }

        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
    }
}
