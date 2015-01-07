using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MongoMS.Controls
{
   public class LastTabSelectionTabControl: TabControl
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (e.NewItems != null)
                if (e.NewItems.Count > 0)
                {
                    SelectedIndex = e.NewStartingIndex;
                   
                }
        }
    }
}
