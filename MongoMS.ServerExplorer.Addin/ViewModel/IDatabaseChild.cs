using System.Collections.ObjectModel;
using MongoMS.Common;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public interface IDatabaseChild
    {
        string Name { get; }
        object CmdParameter { get; }
        ObservableCollection<IMenuCommand> Menu { get; }
    }
}