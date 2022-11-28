//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Net.Http.Formatting;
//using System.Net.Http.Json;
//using _0_Framework.Application;
//using _0_Framework.Application.ZarinPal;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Nancy.Json;
//using Query.Contracts.CartServices;
//using Query.Contracts.Product;
//using System.Text.Json.Serialization;
//using ShopManagement.Application;
//using ShopManagement.Application.Contracts.Order;
//using Parbad;
//using Parbad.Gateway.ParbadVirtual;
//using Parbad.AspNetCore;

//namespace ServiceHost.Pages
//{
//    //[Authorize]
//    public class Checkout2Model : PageModel
//    {
//        [TempData]
//        public string PayAmount { get; set; }
//        [TempData]
//        public string GatewayTrackNo { get; set; }
//        [TempData]
//        public string OrderId { get; set; }
//        [TempData]
//        public string Message { get; set; }

//        public const string CookieName = "cart-items";
//        public Cart Cart;

//        private readonly IAuthHelper _authHelper;
//        private readonly ICartService _cartService;
//        private readonly IProductQuery _productQuery;
//        private readonly IOrderApplication _orderApp;
//        private readonly ILogger<CheckoutModel> logger;
//        private readonly IConfiguration _configuration;
//        private readonly IOnlinePayment _onlinePayment;
//        private readonly IZarinPalFactory _zarinPalFactory;
//        private readonly IHttpClientFactory httpClientFactory;
//        private readonly ICartCalculatorService _cartCalculatorService;

//        public Checkout2Model(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery,
//            IOrderApplication orderapp, IAuthHelper authHelper, IZarinPalFactory zarinPalFactory,IOnlinePayment onlinePayment,
//            IConfiguration configuration, ILogger<CheckoutModel> logger, IHttpClientFactory httpClientFactory)
//        {
//            _orderApp = orderapp;
//            _authHelper = authHelper;
//            _cartService = cartService;
//            _productQuery = productQuery;
//            _onlinePayment = onlinePayment;
//            _configuration = configuration;
//            _zarinPalFactory = zarinPalFactory;
//            _cartCalculatorService = cartCalculatorService;

//            Cart ??= new Cart();
//            this.logger = logger;
//            this.httpClientFactory = httpClientFactory;
//        }

//        public void OnGet()
//        {
//            var serializer = new JavaScriptSerializer();
//            var value = Request.Cookies[CookieName];
//            var cartItems = serializer.Deserialize<List<CartItem>>(value);

//            foreach (var item in cartItems)
//                item.CalculateTotalItemPrice();

//            Cart = _cartCalculatorService.ComputeCart(cartItems);

//            _cartService.Set(Cart);
//        }

//        public async Task<IActionResult> OnPostPay()
//        {
//            var cart = _cartService.Get();
//            var result = _productQuery.CheckInventoryStatus(cart.Items);

//            if (result.Any(CI => CI.IsInStock) == false)
//                return RedirectToPage("/Cart");

//            var orderId = _orderApp.PlaceOrder(cart);

//            OrderId = orderId.ToString();

//            var CallbackURL = Url.Page("./Checkout", "Callback", null, Request.Scheme, Request.Host.Value);


//            var payresult = await
//                _onlinePayment.RequestAsync(invoice =>
//                               invoice
//                               .UseAutoRandomTrackingNumber()
//                               .SetCallbackUrl(CallbackURL)
//                               .SetAmount((Money)cart.PayAmount)
//                               .SetGateway("ParbadVirtualGateway")
//                               .UseParbadVirtual()
//            );

//            GatewayTrackNo = payresult.TrackingNumber.ToString();

//            var url = payresult.GatewayTransporter.TransportToGateway();

//            return payresult.GatewayTransporter.TransportToGateway();
//        }

//        public async Task<IActionResult> OnGetCallBack()
//        {
//            var payresult = new PaymentResult();

//            var invoice = await _onlinePayment.FetchAsync();

//            // Check if the invoice is new or it's already processed before.
//            if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
//            {
//                // You can also see if the invoice is already verified before.
//                var isAlreadyVerified = invoice.IsAlreadyVerified;
//                Response.Cookies.Delete("cart-items");

//                payresult = payresult.Failed(ApplicationMessages.UnSuccessfulPayment);
//                return RedirectToPage("/PaymentResult", payresult);
//            }


//            var verificationResponse = await _onlinePayment.VerifyAsync(long.Parse(GatewayTrackNo));

//            var RefId = verificationResponse.TrackingNumber;
//            var TrackingNo = verificationResponse.TransactionCode;
//            var oId = int.Parse(OrderId);

//            if (verificationResponse.IsSucceed)
//            {
//                var issueTrackingNo = _orderApp.PaymentSucceeded(oId);

//                Response.Cookies.Delete("cart-items");
//                payresult = payresult.Succeeded(ApplicationMessages.SuccessfulPayment, issueTrackingNo);

//                return RedirectToPage("/PaymentResult", payresult);
//            }
//            else
//            {
//                Response.Cookies.Delete("cart-items");

//                payresult = payresult.Failed(ApplicationMessages.UnSuccessfulPayment);
//                return RedirectToPage("/PaymentResult", payresult);
//            }
//        }
//        public async Task<IActionResult> OnPostCallBack()
//        {
//            var payresult = new PaymentResult();

//            var invoice = await _onlinePayment.FetchAsync();

//            // Check if the invoice is new or it's already processed before.
//            if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
//            {
//                // You can also see if the invoice is already verified before.
//                var isAlreadyVerified = invoice.IsAlreadyVerified;
//                Response.Cookies.Delete("cart-items");

//                payresult = payresult.Failed(ApplicationMessages.UnSuccessfulPayment);
//                return RedirectToPage("/PaymentResult", payresult);
//            }

//            var verificationResponse = await _onlinePayment.VerifyAsync(long.Parse(GatewayTrackNo));

//            var RefId = verificationResponse.TrackingNumber;
//            var TrackingNo = verificationResponse.TransactionCode;
//            var oId = int.Parse(OrderId);

//            if (verificationResponse.IsSucceed)
//            {
//                var issueTrackingNo = _orderApp.PaymentSucceeded(oId);

//                Response.Cookies.Delete("cart-items");
//                payresult = payresult.Succeeded(ApplicationMessages.SuccessfulPayment, issueTrackingNo);

//                return RedirectToPage("/PaymentResult", payresult);
//            }
//            else
//            {
//                Response.Cookies.Delete("cart-items");

//                payresult = payresult.Failed(ApplicationMessages.UnSuccessfulPayment);
//                return RedirectToPage("/PaymentResult", payresult);
//            }
//        }




//        //public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
//        //    [FromQuery] long oId)
//        //{
//        //    var orderAmount = _orderapp.GetAmountBy(oId);
//        //    var verificationResponse =
//        //        _zarinPalFactory.CreateVerificationRequest(authority,
//        //            orderAmount.ToString(CultureInfo.InvariantCulture));

//        //    var result = new PaymentResult();
//        //    if (status == "OK" && verificationResponse.Status >= 100)
//        //    {
//        //        var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
//        //        Response.Cookies.Delete("cart-items");
//        //        result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
//        //        return RedirectToPage("/PaymentResult", result);
//        //    }

//        //    result = result.Failed(
//        //        "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
//        //    return RedirectToPage("/PaymentResult", result);
//        //}
//    }
//}
