using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Переименовать поле")]
    class RenameFieldsViewModel : OKVMB
    {
        private readonly MongoCollection _coll;

        public RenameFieldsViewModel(MongoCollection coll)
        {
            _coll = coll;
            AssignCommands<NoWeakRelayCommand>();
        }

        public string OldName { get; set; }
        public string NewName { get; set; }
      
        protected override void OK()
        {
       //TODO test rename
            UpdateDocument ud = new UpdateDocument {{"$rename", new BsonDocument(OldName, NewName)}};
            _coll.Update(new QueryDocument(), ud);
            CloseThisTab();
        }

        void CloseThisTab()
        {
            //TODO move method to base class - its usable
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
        }
    }

}
