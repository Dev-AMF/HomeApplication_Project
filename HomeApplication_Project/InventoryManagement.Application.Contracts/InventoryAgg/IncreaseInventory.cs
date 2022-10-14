using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class IncreaseInventory
    {
        public int InventoryId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.MustBeUnsigned)]
        public int Count { get; set; }
        
        public string Description { get; set; }
    }
}
