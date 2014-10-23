using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.Common
{
    interface IMenuItemAddin
    {
        Type View { get; }
        Type ViewModel { get; }
    }
}
