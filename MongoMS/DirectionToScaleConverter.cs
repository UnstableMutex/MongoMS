using System;
using System.Globalization;
using System.Windows.Data;
using MongoDB.Driver.Linq;

namespace MongoMS
{
    class DirectionToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = (OrderByDirection) value;
            switch (d)
            {
                case OrderByDirection.Ascending:
                    return 1;
                case OrderByDirection.Descending:
                    return -1;
                default:
                    throw new Exception();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}