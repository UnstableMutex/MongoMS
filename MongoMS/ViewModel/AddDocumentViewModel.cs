using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
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
        public ICommand AddGuidIdCommand { get; private set; }

        void AddGuidId()
        {
            BsonDocument d=new BsonDocument();
            if (!string.IsNullOrEmpty(Document))
            {
                d= BsonDocument.Parse(Document);
            }
           
            d["_id"] = Guid.NewGuid();
            Document = d.ToString();
        }
        public ICommand AddDocumentCommand { get; private set; }

        void AddDocument()
        {
          
            _coll.Insert(BsonDocument.Parse(Document));
            Document = string.Empty;
        }

    }
}
