using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS
{
    class QRToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder sb = new StringBuilder();
            var ie = value as IEnumerable<BsonDocument>;
            foreach (var val in ie)
            {
                sb.Append(val.ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine);
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