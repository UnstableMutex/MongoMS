using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS
{
    public  class CommandLevelAttribute:System.Attribute
    {
      public Level Level { get;private set; }

      public CommandLevelAttribute(Level level)
      {
          Level = level;
      }
    }
}
