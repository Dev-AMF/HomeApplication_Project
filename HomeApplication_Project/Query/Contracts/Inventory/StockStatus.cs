﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Inventory
{
    public class StockStatus
    {
        public bool IsStock { get; set; }
        public string ProductName { get; set; }
    }
}
