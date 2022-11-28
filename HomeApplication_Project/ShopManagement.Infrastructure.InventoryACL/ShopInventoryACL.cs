using _0_Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.InventoryACL
{
    public class ShopInventoryACL : IShopInventoryACL
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryACL(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = items.Select(orderItem =>
                    new DecreaseInventory(orderItem.ProductId, orderItem.Count, ApplicationMessages.CustomerPurchase, orderItem.OrderId)).ToList();

            return _inventoryApplication.Decrease(command).IsSucceded;
        }
    }
}
