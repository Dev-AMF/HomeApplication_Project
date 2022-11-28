using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public int PaymentMethodId { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(int accountId, int paymentMethodId, double totalAmount, double discountAmount, double payAmount)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            PaymentMethodId = paymentMethodId;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;

            SetRefId(refId);
        }

        public void SetRefId (long refId)
        {
            if (RefId == 0)
                RefId = refId;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void SetIssueTrackingNo(string number)
        {
            if (IssueTrackingNo == null)
            IssueTrackingNo = number;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
