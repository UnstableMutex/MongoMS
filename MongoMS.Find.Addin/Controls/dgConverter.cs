using System;
using System.Globalization;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS.Find.Addin.Controls
{
    public class dgConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value.GetType();
            if (t == typeof (BsonDateTime))
            {
                var d = (BsonDateTime) value;
                var lt=  d.ToLocalTime();
                return lt.ToShortDateString();
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}