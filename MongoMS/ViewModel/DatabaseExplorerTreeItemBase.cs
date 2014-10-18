using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMLight.Extras;

namespace MongoMS.ViewModel
{


  


   public class DatabaseExplorerTreeItemBase:VMB
    {
       private readonly ItemType _itemType;

       public DatabaseExplorerTreeItemBase(string name="",ItemType itemType= ItemType.Server)
        {
            _itemType = itemType;
            Children = new ObservableCollection<DatabaseExplorerTreeItemBase>();

            Name = name;
        }
        public string Name { get; set; }
        public ObservableCollection<DatabaseExplorerTreeItemBase> Children { get; set; }
        public ItemType Type { get { return _itemType; } }

    }
}
