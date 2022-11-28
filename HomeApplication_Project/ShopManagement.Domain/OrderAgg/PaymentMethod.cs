using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.OrderAgg
{
    public class PaymentMethod : EntityBase
    {
        
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected PaymentMethod()
        {

        }
    }
}
