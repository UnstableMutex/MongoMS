﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    [Header("Статистика")]
    class CollectionStatsViewModel
    {
        private readonly MongoCollection<BsonDocument> _coll;

        private CollectionStatsResult stats;

        public CollectionStatsViewModel(MongoCollection<BsonDocument> coll)
        {
            _coll = coll;

            stats = _coll.GetStats();
        }
    }
}
