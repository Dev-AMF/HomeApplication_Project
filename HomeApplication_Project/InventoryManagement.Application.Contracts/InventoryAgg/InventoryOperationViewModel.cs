namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class InventoryOperationViewModel
    {
        public int Id { get; set; }
        public bool Operation { get; set; }
        public int Count { get; set; }
        public int OperatorId { get; set; }
        public string Operator { get; set; }
        public string OperationDate { get; set; }
        public int CurrentCount { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
    }
}
