using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class CreateInventory
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> ProductViewModels { get; set; }
         
    }
}
