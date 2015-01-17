using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.TTL.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public MainViewModel(MongoCollection collection)
        {
            _collection = collection;
        }

        public override string Header
        {
            get { return "TTL"; }
        }
    }
}
