using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace MongoMS.ServerExplorer.Addin
{
    public static class UnityHolder
    {
        public static IUnityContainer Unity { get; set; }
    }
}
