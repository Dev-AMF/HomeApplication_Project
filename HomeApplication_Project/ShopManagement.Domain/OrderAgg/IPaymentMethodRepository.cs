using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IPaymentMethodRepository : IRepository<int, PaymentMethod>
    {
    }
}
