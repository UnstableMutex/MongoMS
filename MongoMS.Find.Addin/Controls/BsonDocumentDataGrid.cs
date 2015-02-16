using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Bson;
namespace MongoMS.Find.Addin.Controls
{
    public class BsonDocumentDataGrid : DataGrid
    {
        static dgConverter c=new dgConverter();
        public BsonDocumentDataGrid()
        {
            _fieldNames = new StringCollection();
        }
        private readonly StringCollection _fieldNames;
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            _fieldNames.Clear();
            foreach (var item in newValue)
            {
                var doc = item as BsonDocument;
                var elements = doc.Elements.Select(x => x.Name);
                _fieldNames.AddUnique(elements);
            }
            GenerateColumns1();
        }
        private void GenerateColumns1()
        {

            this.Columns.Clear();
            foreach (var fn in _fieldNames)
            {
                var column = new DataGridTextColumn {Binding = new Binding("[" + fn + "]"){Converter = c}, Header = fn};
                Columns.Add(column);
            }
        }
    }
}
