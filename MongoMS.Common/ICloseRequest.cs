using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

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
