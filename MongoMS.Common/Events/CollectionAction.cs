using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoMS.Common.Events
{
   public class CollectionAction
    {
        public MongoDatabase Database { get; set; }
        public string CollectionName { get; set; }
        public ActionType Action { get; set; }
    }
}
