using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountManagement.Domain.CustomerAgg
{
    public class CustomerDiscount : EntityBase
    {
        public int ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }

        public CustomerDiscount(int productId, int discountRate, DateTime startDate,
            DateTime endDate, string description)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public void Edit(int productId, int discountRate, DateTime startDate,
            DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Description = reason;
        }
    }
}
