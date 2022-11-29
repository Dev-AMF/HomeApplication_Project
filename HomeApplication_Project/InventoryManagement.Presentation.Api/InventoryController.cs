using _0_Framework.Application;
using InventoryManagement.Application.Contracts.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Inventory;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery)
        {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }

        [HttpGet("{inventoryId}")]
        public List<InventoryOperationViewModel> GetOperations(int inventoryId)
        {
            return _inventoryApplication.GetOperationsLog(inventoryId);
        }

        [HttpPost]
        public StockStatus CheckStock(IsInStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }

        
        [HttpGet("PriceFormatter/{Price}")]
        public string PriceFormatter(double Price)
        {
            return Price.ToMoney();
        }
    }
}
