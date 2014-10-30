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
    class RenameFieldsViewModel : VMB
    {
        private readonly MongoCollection _coll;

        public RenameFieldsViewModel(MongoCollection coll)
        {
            _coll = coll;
            AssignCommands<NoWeakRelayCommand>();
        }

        public string OldName { get; set; }
        public string NewName { get; set; }
        public ICommand OKCommand { get; private set; }

        private void OK()
        {
       //TODO test rename
            UpdateDocument ud = new UpdateDocument();
            ud.Add("$rename", new BsonDocument(OldName, NewName));
            _coll.Update(new QueryDocument(), ud);
        }
    }
}
