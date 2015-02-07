using System.Collections.Generic;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    [Header("Подробно")]
    [CommandLevel(Level.Database)]
    internal class DatabaseDetailsViewModel : VMB
    {
        private readonly MongoDatabase _db;


        public DatabaseDetailsViewModel(MongoDatabase db)
        {
            _db = db;

            DatabaseStatsResult stats = db.GetStats();

            Size = new Dictionary<string, long>();
            Size.Add("Индексы", stats.IndexSize);
            Size.Add("Данные", stats.DataSize);
            Size.Add("Зарезервированное место", stats.FileSize - stats.IndexSize - stats.DataSize);
        }

        public Dictionary<string, long> Size { get; set; }
    }
}