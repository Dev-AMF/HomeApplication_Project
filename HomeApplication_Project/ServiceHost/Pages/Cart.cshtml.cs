using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using Query.Contracts.Product;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
             CartItems ??= new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            if (cartItems == null) cartItems = new List<CartItem>();

            if (cartItems.Count < 1) Message = ApplicationMessages.EmptyCart;
            else Message = null;

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();


            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }
        public void OnPost()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            if (cartItems == null) cartItems = new List<CartItem>();

            if (cartItems.Count < 1) Message = ApplicationMessages.EmptyCart;
            else Message = null;

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();


            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }


        public IActionResult OnPostRemoveFromCart(int id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(CI => CI.Id == id);
            cartItems.Remove(itemToRemove);

            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);

            var value2 = Request.Cookies[CookieName];
            //var value3 = Response.Cookies.

            return RedirectToPage("/Cart");
        }


        public IActionResult OnPostGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
           
            return RedirectToPage(CartItems.Any(CI => !CI.IsInStock) ? "/Cart" : "/Checkout");
        }
        public IActionResult OnGetRedirectToProductPage(int id)
        {
            var slug = _productQuery.GetSlugBy(id);

            return RedirectToPage("/ProductDetails", new { slug });
        }
    }
}
