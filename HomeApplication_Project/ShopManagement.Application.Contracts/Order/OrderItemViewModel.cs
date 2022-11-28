using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public int DiscountRate { get; set; }
        public int OrderId { get; set; }
    }
}
