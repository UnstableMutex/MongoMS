using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.RenameFields.Addin.ViewModel
{
    public class MainViewModel:CollectionOKViewModel
    {
       
        public MainViewModel(MongoCollection collection):base(collection)
        {
           
        }
     
        protected override void OK()
        {
            base.OK();
            RaiseCloseRequest();
        }
       
      
    }
}
