using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.Find.Addin.ViewModel
{
    public class FindViewModel:CollectionOKViewModel
    {
        private IEnumerable<BsonDocument> _queryResults;

        public FindViewModel(MongoCollection collection):base(collection)
        {
            
        }

        public override string Header
        {
            get { return "Find"; }
        }

        protected override void OK()
        {
            QueryResults = Collection.FindAllAs<BsonDocument>();
        }

        public IEnumerable<BsonDocument> QueryResults
        {
            get { return _queryResults; }
            set
            {
                this.SetProperty(ref _queryResults, value);
            }
        }
    }
}
