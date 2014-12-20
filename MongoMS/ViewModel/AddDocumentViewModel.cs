using System;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Новый документ")]
    internal class AddDocumentViewModel : VMBValidated
    {
        private readonly MongoCollection<BsonDocument> _coll;

        private BsonDocument _doc;
        private string _document;

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

        public ICommand AddDecFieldCommand { get; private set; }

        public ICommand AddFloatFieldCommand { get; private set; }

        public ICommand AddFieldCommand { get; private set; }

        public ICommand AddGuidIdCommand { get; private set; }


        public ICommand AddObjectIdCommand { get; private set; }
        public ICommand AddDocumentCommand { get; private set; }

        private void AddIntField()
        {
            AddField(new BsonElement(FieldName, int.Parse(FieldValue)));
        }

        private void AddDecField()
        {
            AddField(new BsonElement(FieldName, decimal.Parse(FieldValue).ToString("F4")));
        }

        private void AddFloatField()
        {
            AddField(new BsonElement(FieldName, float.Parse(FieldValue)));
        }

        private void AddField()
        {
            AddField(new BsonElement(FieldName, FieldValue));
        }

        private void AddField(BsonElement element)
        {
            if (Doc.Contains(element.Name))
            {
                Doc.Remove(element.Name);
            }
            Doc.Add(element);
            RaisePropertyChangedNoSave("Document");
        }

        private void AddObjectId()
        {
            var g = new ObjectIdGenerator();
            var oid = (ObjectId) g.GenerateId(_coll, Doc);
            AddID(oid);
        }


        private void AddGuidId()
        {
            var guidgen = new GuidGenerator();
            var id = (Guid) guidgen.GenerateId(_coll, Doc);
            AddID(id);
        }

        private void AddID(BsonValue id)
        {
            AddField(new BsonElement("_id", id));
        }

        private void AddDocument()
        {
            _coll.Insert(Doc);
            Doc = new BsonDocument();
        }
    }
}