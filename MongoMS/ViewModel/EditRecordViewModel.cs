using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Правка записи")]
    internal class EditRecordViewModel:VMB
    {
        private readonly MongoCollection<BsonDocument> _coll;
        private readonly BsonDocument _selected;

        public EditRecordViewModel(MongoCollection<BsonDocument> coll, BsonDocument selected)
        {
            _coll = coll;
            _selected = selected;
            AssignCommands<NoWeakRelayCommand>();
        }
        public string DocumentString { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public ICommand GenerateGuidCommand { get; private set; }
        void GenerateGuid()
        { }
    }
}