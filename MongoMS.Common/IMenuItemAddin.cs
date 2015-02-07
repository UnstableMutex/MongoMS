using System;

namespace MongoMS.Common
{
    internal interface IMenuItemAddin
    {
        Type View { get; }
        Type ViewModel { get; }
    }
}