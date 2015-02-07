using System;

namespace MongoMS
{
    public class SelectedEventArgs : EventArgs
    {
        public SelectedEventArgs(object selected)
        {
            Selected = selected;
        }

        public object Selected { get; private set; }
    }
}