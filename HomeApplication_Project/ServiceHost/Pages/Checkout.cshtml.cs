using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Query.Contracts.CartServices;
using Query.Contracts.Product;
using System.Text.Json.Serialization;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using Parbad;
using Parbad.Gateway.ParbadVirtual;
using Parbad.AspNetCore;
using ShopManagement.Domain.OrderAgg;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        
        public List<PaymentMethod> PaymentMethods { get; set; }

        [TempData]
        public string PayAmount { get; set; }
        [TempData]
        public string GatewayTrackNo { get; set; }
        [TempData]
        public string OrderId { get; set; }
        [TempData]
        public string Message { get; set; }

        public const string CookieName = "cart-items";
        public Cart Cart;

        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApp;
        private readonly ILogger<CheckoutModel> logger;
        private readonly IConfiguration _configuration;
        private readonly IOnlinePayment _onlinePayment;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly IPaymentMethodApplication<int , PaymentMethod> _paymentMethodApplication;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery,
            IOrderApplication orderapp, IAuthHelper authHelper, IZarinPalFactory zarinPalFactory, IOnlinePayment onlinePayment,
            IConfiguration configuration, ILogger<CheckoutModel> logger, IHttpClientFactory httpClientFactory, 
            IPaymentMethodApplication<int, PaymentMethod> paymentMethodApplication)
        {
            _orderApp = orderapp;
            _authHelper = authHelper;
            _cartService = cartService;
            _productQuery = productQuery;
            _onlinePayment = onlinePayment;
            _configuration = configuration;
            _zarinPalFactory = zarinPalFactory;
            _cartCalculatorService = cartCalculatorService;

            Cart ??= new Cart();
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            _paymentMethodApplication = paymentMethodApplication;
        }

        public void OnGet()
        {
            PaymentMethods = _paymentMethodApplication.GetAll();

            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            Cart = _cartCalculatorService.ComputeCart(cartItems);

            _cartService.Set(Cart);
        }

        public  IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod); 
            
            var result = _productQuery.CheckInventoryStatus(cart.Items);

            if (result.Any(CI => CI.IsInStock) == false)
                return RedirectToPage("/Cart");

            var orderId = _orderApp.PlaceOrder(cart);

            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(CultureInfo.InvariantCulture)
                                                                       , "", "", ApplicationMessages.GatewayDescription, orderId);
                return Redirect(
                        $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            else
            {
                var refId = CodeGenerator.GenerateNumericCode();
                 _orderApp.SetRefId(orderId, refId);
              
                Response.Cookies.Delete("cart-items");
                
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult",
                    paymentResult.Succeeded(string.Format(ApplicationMessages.CashPayDescription,refId), null));
            }

        }


        public IActionResult OnGetCallBack([FromQuery] int oId, [FromQuery] string authority, [FromQuery] string status)
        {
            var orderAmount = _orderApp.GetAmountBy(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApp.PaymentSucceeded(oId);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded(ApplicationMessages.SuccessfulPayment, issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(ApplicationMessages.UnSuccessfulPayment);
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
