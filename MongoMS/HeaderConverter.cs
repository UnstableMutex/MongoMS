using System;
using System.Globalization;
using System.Windows.Data;
using MVVMTemplateSelection;

namespace MongoMS
{
    internal class HeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type t = value.GetType();
            try
            {
                string h = t.GetCustomAttribute<HeaderAttribute>().Header;
                return h;
            }
            catch (InvalidOperationException)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}