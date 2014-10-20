using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Bson;
namespace MongoMS.View
{
    class CustomDataGrid : DataGrid
    {
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            HashSet<string> hs = new HashSet<string>();
            var nv = newValue as IEnumerable<BsonDocument>;
            if (nv != null)
            {
                this.Columns.Clear();
                //var doc = (newValue as IEnumerable<BsonDocument>).First();
                foreach (var doc in nv)
                {
                    foreach (var ele in doc.Elements)
                    {
                        hs.Add(ele.Name);
                    }
                }
                foreach (var h in hs)
                {
                    this.Columns.Add(new DataGridTextColumn() { Binding = new Binding("[" + h + "]") { Converter = new bsonConverter() }, Header = h });
                }
            }
        }
    }
    internal class bsonConverter : IValueConverter
    {
        private Type t;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            t = value.GetType();
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = BsonValue.Create(value);
            return val;
        }
    }
}
