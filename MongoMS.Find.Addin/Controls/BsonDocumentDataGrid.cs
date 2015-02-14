﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Bson;

namespace MongoMS.Find.Addin.Controls
{
    public class BsonDocumentDataGrid : DataGrid
    {
        public BsonDocumentDataGrid()
        {
           
            fieldNames = new StringCollection();
        }

       

        private StringCollection fieldNames;
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            fieldNames.Clear();
            foreach (var item in newValue)
            {
                var doc = item as BsonDocument;
                var elements = doc.Elements.Select(x => x.Name);
                fieldNames.AddUnique(elements);
            }
           
            GenerateColumns1();
        }

        private void GenerateColumns1()
        {

            this.Columns.Clear();
            foreach (var fn in fieldNames)
            {
                var column = new DataGridTextColumn();
                column.Binding = new Binding("[\""+fn+"\"]");
                column.Header = "asdads";
                Columns.Add(column);
            }
        }

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {

        }

        protected override void OnAutoGeneratedColumns(EventArgs e)
        {

        }
    }
  

    public static class ext
    {
        public static void AddUnique(this StringCollection sc, IEnumerable<string> item)
        {
            foreach (var i in item)
            {
                AddUnique(sc, i);
            }
        }

        public static void AddUnique(this StringCollection sc, string item)
        {
            if (sc.IndexOf(item) < 0)
            {
                sc.Add(item);
            }
        }
    }
}
