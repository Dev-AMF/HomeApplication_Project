namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class DecreaseInventory
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
    }
}
