using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class MakeCollectionCappedViewModel:VMB
    {
        private readonly MongoCollection _coll;

        public MakeCollectionCappedViewModel(MongoCollection coll)
        {
            _coll = coll;
        }

        public ICommand OKCommand { get; private set; }

        private void OK()
        {
            var g = Guid.NewGuid();
          var s=  g.ToString();
            CollectionOptionsBuilder b=new CollectionOptionsBuilder();
            b.SetCapped(true).SetMaxDocuments(MaxCount).SetMaxSize(MaxSize);
          _coll.Database.CreateCollection(s, b);
            
            var newcoll = _coll.Database.GetCollection(s);
            foreach (var item in _coll.FindAllAs<BsonDocument>())
            {
                newcoll.Save(item);
            }
            _coll.Drop();
        }

        public int MaxSize { get; set; }
        public int MaxCount { get; set; }

    }
}
