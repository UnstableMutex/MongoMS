using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("Статистика")]
    class CollectionStatsViewModel
    {
        private readonly string _cs;
        private readonly string _db;
        private readonly string _coll;
        private CollectionStatsResult stats;

        public CollectionStatsViewModel(string cs, string db, string coll)
        {
            _cs = cs;
            _db = db;
            _coll = coll;
             stats = new MongoClient(_cs).GetServer().GetDatabase(_db).GetCollection(_coll).GetStats();
         
        }

    }
}
