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
