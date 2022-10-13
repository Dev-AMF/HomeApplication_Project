using InventoryManagement.Application.Contracts.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository
    {
        EditInventory GetDetails(int id);
        Inventory GetBy(int productId);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
