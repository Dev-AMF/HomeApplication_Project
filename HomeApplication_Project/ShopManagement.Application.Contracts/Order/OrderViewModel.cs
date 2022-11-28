using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel 
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountFullName { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
        public string IssueTrackingNo { get; set; }
        public long RefId { get; set; }
        public string CreationDate { get; set; }
    }
}
