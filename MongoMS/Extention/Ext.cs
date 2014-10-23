using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoMS.Extention
{
    static class Ext
    {
        public static BsonValue GetMongoType(IDataRecord r, int index)
        {
            throw new NotImplementedException();
        }
    }
    public static class TypeExtensions
    {
        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>The custom attribute of type T, if found.</returns>
        public static T GetAttribute<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            return GetAttributes<T>(typeWithAttributes).FirstOrDefault();
        }

        public static T GetAttribute<T>(this PropertyInfo pi)
            where T : Attribute
        {
         return   pi.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;





        }
        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>An enumeration of attributes of type T that were found.</returns>
        public static IEnumerable<T> GetAttributes<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            // Try to find the configuration attribute for the default logger if it exists
            object[] configAttributes = Attribute.GetCustomAttributes(typeWithAttributes,
                typeof(T), false);

            if (configAttributes != null)
            {
                foreach (T attribute in configAttributes)
                {
                    yield return attribute;
                }
            }
        }
    }
}

