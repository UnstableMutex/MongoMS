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
        private BsonDocument _doc;

        public AddDocumentViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;
            Doc=new BsonDocument();
            AssignCommands<NoWeakRelayCommand>();
        }


        [BsonDocumentValidator("sfdsfsdf")]
        public string Document
        {
            get { return Doc.ToString(); }
          
        }

        private BsonDocument Doc
        {
            get { return _doc; }
            set
            {
                _doc = value;
               RaisePropertyChangedNoSave("Document");
            }
        }

        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public ICommand AddFieldCommand { get; private set; }

        void AddField()
        {
            AddField(new BsonElement(FieldName,FieldValue));
        }
        public ICommand AddGuidIdCommand { get; private set; }


        void AddField(BsonElement element)
        {
            Doc.Add(element);
            RaisePropertyChangedNoSave("Document");
        }


        void AddGuidId()
        {

            AddField(new BsonElement("_id",Guid.NewGuid()));




            //BsonDocument d=new BsonDocument();
            //if (!string.IsNullOrEmpty(Document))
            //{
            //    d= BsonDocument.Parse(Document);
            //}
           
            //d["_id"] = Guid.NewGuid();
            //Document = d.ToString();
        }
        public ICommand AddDocumentCommand { get; private set; }

        void AddDocument()
        {
          
            _coll.Insert(Doc);
           Doc=new BsonDocument();
        }

    }
}
