using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class AddDocumentViewModel : VMBValidated
    {
        private readonly string _cs;
        private readonly string _db;
        private readonly string _coll;
        private string _document;

        public AddDocumentViewModel(string cs, string db, string coll)
        {
            _cs = cs;
            _db = db;
            _coll = coll;
            AssignCommands<NoWeakRelayCommand>();
        }
        [BsonDocumentValidator("sfdsfsdf")]
        public string Document
        {
            get { return _document; }
            set
            {
                _document = value;
                try
                {
                    RaisePropertyChanged();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public ICommand AddDocumentCommand { get; private set; }

        void AddDocument()
        {
            var coll = new MongoClient(_cs).GetServer().GetDatabase(_db).GetCollection(_coll);
            coll.Insert(BsonDocument.Parse(Document));
        }

    }
}
