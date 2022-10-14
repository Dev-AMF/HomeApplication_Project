using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<int, Inventory>
    {
        EditInventory GetDetails(int id);
        Inventory GetBy(int productId);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationsLog(int inventoryId);
    }
}
