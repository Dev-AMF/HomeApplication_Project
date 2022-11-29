using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Inventory
{
    public class IsInStock 
    {
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
