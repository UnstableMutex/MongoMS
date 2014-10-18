using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MVVMLight.Extras;

namespace MongoMS
{
    class BsonDocumentValidator : Validator
    {
        public BsonDocumentValidator(string message)
            : base(message)
        {
        }

        public override bool Validate(object value)
        {
            var s = value.ToString();
            try
            {
                var doc = BsonDocument.Parse(s);
                return true;
            }
            catch (FileFormatException)
            {

                return false;
            }
            catch (InvalidOperationException)
            {
                 return false;
            }


        }
    }
}
