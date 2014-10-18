using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerServerViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly string _cs;

        public DatabaseExplorerServerViewModel(string name,string cs):base(name, ItemType.Server)
        {
            _cs = cs;
            GetDatabases();
        }

        private void GetDatabases()
        {
            var dbs = (new MongoClient(_cs).GetServer().GetDatabaseNames());
            foreach (var db in dbs)
            {
                    Children.Add(new DatabaseExplorerDatabaseViewModel(db,_cs));

            }
        }
    }
}