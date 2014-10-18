namespace MongoMS.ViewModel
{
    class DatabaseExplorerCollectionViewModel : DatabaseExplorerTreeItemBase
    {
        public DatabaseExplorerCollectionViewModel(string name):base(name, ItemType.Collection)
        {
            
        }
    }
}