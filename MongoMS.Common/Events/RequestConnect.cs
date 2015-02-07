namespace MongoMS.Common.Events
{
   public class RequestConnect
    {
       private readonly string _name;
       private readonly string _connectionString;

       public RequestConnect(string name,string connectionString)
       {
           _name = name;
           _connectionString = connectionString;
       }

       public string Name
       {
           get { return _name; }
       }

       public string ConnectionString
       {
           get { return _connectionString; }
       }
    }
}
