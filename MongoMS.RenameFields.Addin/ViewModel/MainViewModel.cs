using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.RenameFields.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public MainViewModel(MongoCollection collection)
        {
            _collection = collection;
        }

        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
    }
}
