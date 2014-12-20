using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Новая коллекция")]
    internal class AddCollectionViewModel : OKVMB
    {
        private readonly MongoDatabase _db;

        private bool _capped;

        public AddCollectionViewModel(MongoDatabase db)
        {
            _db = db;
            AssignCommands<NoWeakRelayCommand>();
        }

        public string Name { get; set; }

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
            var b = new CollectionOptionsBuilder();
            if (Capped)
            {
                b.SetCapped(Capped);
            }
            if (MaxSize.HasValue)
            {
                b.SetMaxSize(MaxSize.Value);
            }
            if (MaxDocuments.HasValue)
            {
                b.SetMaxDocuments(MaxDocuments.Value);
            }
            _db.CreateCollection(Name, b);
            MongoCollection<BsonDocument> mongoCollection = _db.GetCollection(Name);
            var coll = new DatabaseExplorerCollectionViewModel(mongoCollection);
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerCollectionViewModel>(this, coll, "added"));
            SimpleIoc.Default.GetInstance<MainViewModel>().Content.Remove(this);
        }
    }
}