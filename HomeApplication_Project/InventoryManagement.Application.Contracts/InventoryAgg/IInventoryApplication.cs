using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public interface IInventoryApplication
    {
        EditInventory GetDetails(int id);

        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
