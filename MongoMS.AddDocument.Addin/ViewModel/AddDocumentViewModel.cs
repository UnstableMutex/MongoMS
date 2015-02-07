using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.AddDocument.Addin.ViewModel
{
    public class AddDocumentViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public AddDocumentViewModel(MongoCollection collection)
        {
            _collection = collection;
        }

        public override string Header
        {
            get { return "Add Doc"; }
        }
    }
}
