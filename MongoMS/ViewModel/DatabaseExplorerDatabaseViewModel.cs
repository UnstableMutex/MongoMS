using MongoDB.Driver;

namespace MongoMS.ViewModel
{
    class DatabaseExplorerDatabaseViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly string _cs;

        public DatabaseExplorerDatabaseViewModel(string name,string cs):base(name, ItemType.Database)
        {
            _cs = cs;
            GetCollections();
        }

        private void GetCollections()
        {
            var colls = new MongoClient(_cs).GetServer().GetDatabase(Name).GetCollectionNames();
            foreach (var coll in colls)
            {
                Children.Add(new DatabaseExplorerCollectionViewModel(coll,_cs,Name));
            }


        }
    }
}