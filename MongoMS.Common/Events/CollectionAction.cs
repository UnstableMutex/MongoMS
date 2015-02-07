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
