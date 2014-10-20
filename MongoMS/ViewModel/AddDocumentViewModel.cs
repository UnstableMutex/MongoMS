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
    [Header("Новый документ")]
    class AddDocumentViewModel : VMBValidated
    {
        private readonly MongoCollection<BsonDocument> _coll;

        private string _document;

        public AddDocumentViewModel(MongoCollection<BsonDocument> coll)
        {
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
          
            _coll.Insert(BsonDocument.Parse(Document));
        }

    }
}
