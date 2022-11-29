using InventoryManagement.Infrastructure.EFCore;
using Query.Contracts.Inventory;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly At_HomeApplicationContext _shopContext;
        private readonly At_HomeApplicationInventoryContext _inventoryContext;

        public InventoryQuery(At_HomeApplicationInventoryContext inventoryContext, At_HomeApplicationContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);

            if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
            {
                var product = _shopContext.Products
                                          .Select(x => new { x.Id, x.Name })
                                          .FirstOrDefault(x => x.Id == command.ProductId);
                return new StockStatus
                {
                    IsStock = false,
                    ProductName = product?.Name
                };
            }

            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}

