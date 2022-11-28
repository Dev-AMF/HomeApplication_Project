using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class DecreaseInventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.MustBeUnsigned)]
        public int Count { get; set; }

        public string Description { get; set; }
        public int OrderId { get; set; }

        public DecreaseInventory()
        {

        }

        public DecreaseInventory(int productId, int count, string description, int orderId)
        {
            ProductId = productId;
            Count = count;
            Description = description;
            OrderId = orderId;
        }
    }
}
