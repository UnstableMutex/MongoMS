using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.TTL.Addin.ViewModel
{
    public class MainViewModel:CollectionOKViewModel
    {
      
        public MainViewModel(MongoCollection collection):base(collection)
        {
           
        }

        public override string Header
        {
            get { return "TTL"; }
        }

        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
    }
}
