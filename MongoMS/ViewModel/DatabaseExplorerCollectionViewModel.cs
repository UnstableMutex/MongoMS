using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.CSharp.RuntimeBinder;
using MVVMLight.Extras;

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
            AssignCommands<NoWeakRelayCommand>();
        }
        public ICommand FindCommand { get; private set; }

        void Find()
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Content = new FindViewModel(_cs,_db,Name);
        }
    }
}