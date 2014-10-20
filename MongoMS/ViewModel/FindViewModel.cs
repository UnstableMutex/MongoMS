using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
   [Header("Поиск")]
    class FindViewModel:VMB
    {
        private readonly string _cs;
        private readonly string _db;
        private readonly string _coll;
       private IEnumerable<BsonDocument> _queryResults;

       public FindViewModel(string cs,string db,string coll)
        {
            _cs = cs;
            _db = db;
            _coll = coll;
            QueryResults = new MongoClient(_cs).GetServer().GetDatabase(_db).GetCollection(_coll).FindAll().SetLimit(100).ToList();
           //var dt = Getdatatable(QueryResults);
        }

      

       public IEnumerable<BsonDocument> QueryResults
       {
           get { return _queryResults; }
           set
           {
               _queryResults = value;
               RaisePropertyChangedNoSave();
           }
       }

       public DataTable dt { get; set; }
    }
}
