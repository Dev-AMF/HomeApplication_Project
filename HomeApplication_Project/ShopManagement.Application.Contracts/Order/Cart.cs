using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public double PayAmount { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }

        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalItemPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmount;
        }
    }
}
