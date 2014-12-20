using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS
{
    internal class QRToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sb = new StringBuilder();
            var ie = value as IEnumerable<BsonDocument>;
            foreach (BsonDocument val in ie)
            {
                sb.Append(val + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
            return sb.ToString();
            // return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}