using System.Collections.ObjectModel;

namespace MongoMS.ViewModel
{
    internal class DatabaseExplorerViewModel
    {
        public DatabaseExplorerViewModel()
        {
            Servers = new ObservableCollection<DatabaseExplorerTreeItemBase>();
            //var i = new DatabaseExplorerTreeItemBase("1", ItemType.Server);
            //i.Children.Add(new DatabaseExplorerTreeItemBase("11", ItemType.Database));
            //Servers.Add(new DatabaseExplorerTreeItemBase("2",ItemType.Server));
            //Servers.Add(i);
            //Servers.Add(new DatabaseExplorerTreeItemBase("3",ItemType.Server));
        }

        public ObservableCollection<DatabaseExplorerTreeItemBase> Servers { get; private set; }
    }
}