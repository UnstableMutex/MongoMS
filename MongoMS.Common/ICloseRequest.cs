using System;

namespace MongoMS.Common
{
    public interface ICloseRequest
    {
        void BeforeClose();
        event EventHandler CloseRequest;
    }
    public interface ITabContent
    {
        string Header { get; }
    }
}
