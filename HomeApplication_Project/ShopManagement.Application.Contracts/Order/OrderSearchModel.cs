using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderSearchModel
    {
        public int AccountId { get; set; }
        public bool IsCanceled { get; set; }
    }
}
