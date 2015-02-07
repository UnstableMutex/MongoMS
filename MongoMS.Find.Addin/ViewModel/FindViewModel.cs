using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.Find.Addin.ViewModel
{
    public class FindViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public FindViewModel(MongoCollection collection)
        {
            _collection = collection;
        }

        public override string Header
        {
            get { return "Find"; }
        }
    }
}
