using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS.View
{
    internal class CustomDataGrid : DataGrid
    {
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            var hs = new HashSet<string>();
            var nv = newValue as IEnumerable<BsonDocument>;
            if (nv != null)
            {
                Columns.Clear();
                //var doc = (newValue as IEnumerable<BsonDocument>).First();
                foreach (BsonDocument doc in nv)
                {
                    foreach (BsonElement ele in doc.Elements)
                    {
                        hs.Add(ele.Name);
                    }
                }
                foreach (string h in hs)
                {
                    Columns.Add(new DataGridTextColumn
                    {
                        Binding = new Binding("[" + h + "]") {Converter = new bsonConverter()},
                        Header = h
                    });
                }
            }
        }
    }
}