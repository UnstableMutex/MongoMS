using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MongoDB.Driver;

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
      public MenuCommand()
      {
        
      }

     

      public string Name { get;  set; }
      public ICommand Command { get;  set; }
    }
}
