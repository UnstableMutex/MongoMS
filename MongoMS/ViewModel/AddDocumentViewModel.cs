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
            Doc = new BsonDocument();
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
        public ICommand AddIntFieldCommand { get; private set; }

        private void AddIntField()
        {
            AddField(new BsonElement(FieldName, int.Parse(FieldValue)));
        }

        public ICommand AddDecFieldCommand { get; private set; }

        private void AddDecField()
        {
            AddField(new BsonElement(FieldName, decimal.Parse(FieldValue).ToString("F4")));
        }
        public ICommand AddFloatFieldCommand { get; private set; }

        private void AddFloatField()
        {
            AddField(new BsonElement(FieldName, float.Parse(FieldValue)));
        }
        public ICommand AddFieldCommand { get; private set; }

        void AddField()
        {
            AddField(new BsonElement(FieldName, FieldValue));
        }
        public ICommand AddGuidIdCommand { get; private set; }


        void AddField(BsonElement element)
        {
            if (Doc.Contains(element.Name))
            {
                Doc.Remove(element.Name);
            }
            Doc.Add(element);
            RaisePropertyChangedNoSave("Document");
        }

        public ICommand AddObjectIdCommand { get; private set; }

        private void AddObjectId()
        {
         
            ObjectIdGenerator g = new ObjectIdGenerator();
            var oid = (ObjectId)g.GenerateId(_coll, Doc);
            AddID(oid);
        }


        void AddGuidId()
        {
            var guidgen = new GuidGenerator();
         var id  = (Guid)guidgen.GenerateId(_coll, Doc);
            AddID(id);
        }

        void AddID(BsonValue id)
        {
            AddField(new BsonElement("_id", id));
        }
        public ICommand AddDocumentCommand { get; private set; }

        void AddDocument()
        {

            _coll.Insert(Doc);
            Doc = new BsonDocument();
        }

    }
}
