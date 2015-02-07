using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using MongoDB.Driver.GridFS;
using MongoMS.Common;

namespace MongoMS.ServerExplorer.Addin.ViewModel
{
    public class GridFSViewModel:IDatabaseChild
    {
        private readonly MongoGridFS _fs;

        public GridFSViewModel(MongoGridFS fs)
        {
            _fs = fs;
        }
        public string Name { get { return "GridFS"; } }
        public ObservableCollection<IMenuCommand> Menu
        {
            get
            {
                var res = UnityHolder.Unity.Resolve<ObservableCollection<IMenuCommand>>(ContextMenuLevel.GridFS.ToString());
                return res;
            }
        }
        public object CmdParameter
        {
            get { return _fs; }
        }
    }
}