using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;
namespace MongoMS.ViewModel
{
    [Header("Новая коллекция")]
    class AddCollectionViewModel:OKVMB
    {
        private readonly MongoDatabase _db;
        public AddCollectionViewModel(MongoDatabase db)
        {
            _db = db;
            AssignCommands<NoWeakRelayCommand>();
        }
        private string _name;
        private bool _capped;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public bool Capped
        {
            get { return _capped; }
            set
            {
                _capped = value;
                RaisePropertyChangedNoSave();
            }
        }
        public long? MaxSize { get; set; }
        public long? MaxDocuments { get; set; }
 
       protected override void OK()
        {
            CollectionOptionsBuilder b=new CollectionOptionsBuilder();
            if (Capped)
            { b.SetCapped(Capped);}
            if (MaxSize.HasValue)
            {
                b.SetMaxSize(MaxSize.Value);
            }
            if (MaxDocuments.HasValue)
            {
                b.SetMaxDocuments(MaxDocuments.Value);
            }
           _db.CreateCollection(Name, b);
            var mongoCollection = _db.GetCollection(Name);
            DatabaseExplorerCollectionViewModel coll=new DatabaseExplorerCollectionViewModel(mongoCollection);
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerCollectionViewModel>(this,coll,"added"));
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
        }
    }
}
