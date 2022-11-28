using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public interface ICartService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
