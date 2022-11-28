using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.Services
{
    public interface IShopInventoryACL
    {
        bool ReduceFromInventory(List<OrderItem> items);
    }
}
