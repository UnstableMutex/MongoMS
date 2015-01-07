using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MongoMS.Common
{
    public class RegionNames
    {
        public const string TabControlRegion = "TabControlRegion";
        public const string TopMenuRegion = "TopMenuRegion";
        public const string ServerExplorerRegion = "ServerExplorerRegion";
    }
    public interface IMenuCommand
    {
        string Name { get; }
        ICommand Command { get; }
    }

  public  class MenuCommand:IMenuCommand
    {
      public MenuCommand(string name)
      {
          Name = name;
      }

      public string Name { get; private set; }
      public ICommand Command { get; private set; }
    }
}
