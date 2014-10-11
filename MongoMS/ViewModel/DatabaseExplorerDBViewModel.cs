using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerDBViewModel
    {
        private readonly string _cs;
        private readonly string _db;

        public DatabaseExplorerDBViewModel(string cs,string db)
        {
            _cs = cs;
            _db = db;

            var dbo = new MongoClient(cs).GetServer().GetDatabase(db).GetCollectionNames();



            Collections = new ObservableCollection<string>(dbo);
        }
        public string Name { get { return _db; } }
        public ObservableCollection<string> Collections { get; private set; } 
    }
}
