using InventoryManagement.Application.Contracts.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication ProductApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = ProductApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProductViewModels(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                ProductViewModels = _productApplication.GetProductViewModels()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.ProductViewModels = _productApplication.GetProductViewModels();
            return Partial("Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(int id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase", command);
        }

        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDecrease(int id)
        {
            var command = new DecreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Decrease", command);
        }

        public JsonResult OnPostReduce(DecreaseInventory command)
        {
            var result = _inventoryApplication.Decrease(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(int id)
        {
            var log = _inventoryApplication.GetOperationsLog(id);
            return Partial("OperationLog", log);
        }
    }
}
