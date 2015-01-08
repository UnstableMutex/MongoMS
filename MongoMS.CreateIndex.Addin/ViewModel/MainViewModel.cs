﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoMS.Common;

namespace MongoMS.CreateIndex.Addin.ViewModel
{
    public class MainViewModel:OKViewModel
    {
        private readonly MongoCollection _collection;

        public MainViewModel(MongoCollection collection)
        {
            _collection = collection;
        }
    }
}