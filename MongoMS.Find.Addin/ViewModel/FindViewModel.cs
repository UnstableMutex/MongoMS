using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.Find.Addin.ViewModel
{
    public class FindViewModel:CollectionOKViewModel
    {
       
        public FindViewModel(MongoCollection collection):base(collection)
        {
            
        }

        public override string Header
        {
            get { return "Find"; }
        }

    }
}
