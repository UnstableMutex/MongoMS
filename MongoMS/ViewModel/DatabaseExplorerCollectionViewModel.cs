namespace MongoMS.ViewModel
{
    class DatabaseExplorerCollectionViewModel : DatabaseExplorerTreeItemBase
    {
        private readonly string _cs;
        private readonly string _db;

        public DatabaseExplorerCollectionViewModel(string name,string cs, string db):base(name, ItemType.Collection)
        {
            _cs = cs;
            _db = db;
        }
    }
}