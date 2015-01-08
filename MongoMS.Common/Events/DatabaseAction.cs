using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.Common.Events
{
   public class DatabaseAction
   {
       public MongoServer Server { get; set; }
       public string DatabaseName { get; set; }
       public ActionType Action { get; set; }
   }
}
