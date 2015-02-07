using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.CreateIndex.Addin.ViewModel
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
            get { return "New Index"; }
        }

        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
    }
}
