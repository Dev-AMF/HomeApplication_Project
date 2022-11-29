using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Inventory
{
    public interface IInventoryQuery 
    {
        StockStatus CheckStock(IsInStock command);
    }
}
