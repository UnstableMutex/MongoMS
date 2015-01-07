﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoMS.Common
{
    public interface ICloseRequest
    {
        void BeforeClose();
    }
    public interface ITabContent
    {
        string Header { get; }
    }
}