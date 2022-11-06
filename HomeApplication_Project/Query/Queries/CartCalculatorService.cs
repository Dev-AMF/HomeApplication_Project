using _0_Framework.Application;
using _0_Framework.Domain;
using DiscountManagement.Infrastructure.EFCore;
using Query.Contracts.CartServices;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly IAuthHelper _authHelper;
        private readonly At_HomeApplicationDiscountContext _discountContext;

        public CartCalculatorService(At_HomeApplicationDiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(COD => !COD.IsRemoved)
                .Select(COD => new { COD.DiscountRate, COD.ProductId })
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(CD => CD.StartDate < DateTime.Now && CD.EndDate > DateTime.Now)
                .Select(CD => new { CD.DiscountRate, CD.ProductId })
                .ToList();
            var currentAccountRole = _authHelper.CurrentAccountRole();

            foreach (var cartItem in cartItems)
            {
                if (currentAccountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(COD => COD.ProductId == cartItem.Id);
                    if (colleagueDiscount != null)
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(CD => CD.ProductId == cartItem.Id);
                    if (customerDiscount != null)
                        cartItem.DiscountRate = customerDiscount.DiscountRate;
                }

                cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }

            return cart;
        }
    }
}
