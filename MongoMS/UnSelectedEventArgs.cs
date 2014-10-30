using System;

namespace MongoMS
{
    class UnSelectedEventArgs : EventArgs
    {
        public UnSelectedEventArgs(object unselected)
        {
            UnSelected = unselected;
        }
        public object UnSelected { get; private set; }
    }
}