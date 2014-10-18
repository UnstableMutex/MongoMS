using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MVVMTemplateSelection;

namespace MongoMS
{
    class HeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value.GetType();

            try
            {
                var h = t.GetCustomAttribute<HeaderAttribute>().Header;
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
