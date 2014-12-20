using System;
using System.IO;
using MongoDB.Bson;
using MVVMLight.Extras;

namespace MongoMS
{
    internal class BsonDocumentValidator : Validator
    {
        public BsonDocumentValidator(string message)
            : base(message)
        {
        }

        public override bool Validate(object value)
        {
            string s = value.ToString();
            try
            {
                BsonDocument doc = BsonDocument.Parse(s);
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