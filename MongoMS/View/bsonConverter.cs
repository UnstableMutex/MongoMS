using System;
using System.Globalization;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS.View
{
    internal class bsonConverter : IValueConverter
    {
        private Type t;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // t = value.GetType();
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BsonValue val = BsonValue.Create(value);
            return val;
        }
    }
}