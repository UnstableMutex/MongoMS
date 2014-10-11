using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerServerViewModel:VMB
    {
        private readonly string _name;
        private readonly string _cs;

        public DatabaseExplorerServerViewModel(string name, string cs)
        {
            _name = name;
            _cs = cs;
            Databases = new ObservableCollection<DatabaseExplorerDBViewModel>();
            var dbs = new MongoClient(_cs).GetServer().GetDatabaseNames();
            foreach (var db in dbs)
            {
                DatabaseExplorerDBViewModel dbvm=new DatabaseExplorerDBViewModel(_cs,db);
                Databases.Add(dbvm);
            }
        }
        public string Name { get { return _name; } }
        public ObservableCollection<DatabaseExplorerDBViewModel> Databases { get; private set; } 

    }
}
