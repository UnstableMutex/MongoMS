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
    class AddCollectionViewModel:VMB
    {
        private readonly string _cs;
        private readonly string _db;

        public AddCollectionViewModel(string cs,string db)
        {
            _cs = cs;
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
        public ICommand OKCommand { get; private set; }

        void OK()
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


            new MongoClient(_cs).GetServer().GetDatabase(_db).CreateCollection(Name, b);



            DatabaseExplorerCollectionViewModel coll=new DatabaseExplorerCollectionViewModel(Name,_cs,_db);
            MessengerInstance.Send(new NotificationMessage<DatabaseExplorerCollectionViewModel>(this,coll,"added"));


            SimpleIoc.Default.GetInstance<MainViewModel>().Content = null;
        }
    }
}
