using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS.View
{
   public class CustomDataGrid:DataGrid
    {
       protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
       {
           base.OnItemsSourceChanged(oldValue, newValue);
           this.Columns.Clear();
           var doc = (newValue as IEnumerable<BsonDocument>).First();
           for (int i = 0; i < doc.Names.Count(); i++)
           {
                this.Columns.Add(new DataGridTextColumn() { Header = doc.Names.ElementAt(i), Binding = new Binding(string.Format("Elements[{0}].Value",i)) });
           }

          
       }
    }
}
