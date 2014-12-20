using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Переименовать поле")]
    internal class RenameFieldsViewModel : CollectionVMB
    {
        public RenameFieldsViewModel(MongoCollection<BsonDocument> coll) : base(coll)
        {
            AssignCommands<NoWeakRelayCommand>();
        }

        public string OldName { get; set; }
        public string NewName { get; set; }

        public override IEnumerable<string> FieldNames
        {
            get { return base.FieldNames.Where(x => x != "_id"); }
        }

        protected override void OK()
        {
            //TODO test rename
            var ud = new UpdateDocument {{"$rename", new BsonDocument(OldName, NewName)}};
            _coll.Update(new QueryDocument(), ud);
            CloseThisTab();
        }
    }
}