using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.CartServices
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
