using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public int Id { get; private set; }
        public bool Operation { get; private set; }
        public int Count { get; private set; }
        public int OperatorId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public int CurrentCount { get; private set; }
        public string Description { get; private set; }
        public int OrderId { get; private set; }
        public int InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, int count, int operatorId, int currentCount,
            string description, int orderId, int invetoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = invetoryId;
            OperationDate = DateTime.Now;
        }
        protected InventoryOperation()
        {

        }
    }

}
