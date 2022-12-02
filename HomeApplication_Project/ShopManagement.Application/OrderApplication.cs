using _0_Framework.Application;
using _0_Framework.Application.Sms;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IRestSmsService _smsService;
        private readonly IConfiguration _configuration;
        private readonly IShopAccountAcl _shopAccountACL;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryACL _shopInventoryACL;

        public OrderApplication(IAuthHelper authHelper, IOrderRepository orderRepository, IConfiguration configuration,
            IShopInventoryACL shopInventoryACL, IRestSmsService smsService, IShopAccountAcl shopAccountACL)
        {
            _authHelper = authHelper;
            _orderRepository = orderRepository;
            _configuration = configuration;
            _shopInventoryACL = shopInventoryACL;
            _smsService = smsService;
            _shopAccountACL = shopAccountACL;
        }

        public int PlaceOrder(Cart cart)
        {

            var currentAccountId = _authHelper.CurrentAccountId();
            var order = new Order(currentAccountId, cart.PaymentMethodId, cart.TotalAmount, cart.DiscountAmount,
                cart.PayAmount);

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.AddItem(orderItem);
            }


            _orderRepository.Create(order);
            _orderRepository.Save();

            return order.Id;
        }

        public string PaymentSucceeded(int orderId)
        {

            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(CodeGenerator.GenerateNumericCode());

            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNo = CodeGenerator.GenerateMixedCode(symbol);

            order.SetIssueTrackingNo(issueTrackingNo);
            
            if (!_shopInventoryACL.ReduceFromInventory(order.Items)) return "";

            _orderRepository.Save();

            var account = _shopAccountACL.GetAccountBy(order.AccountId);

            //var SmsMessage = string.Format(ApplicationMessages.PurchaseSms, account.Fullname, order.IssueTrackingNo);
            //var SmsResult = _smsService.Send(SmsMessage, account.MobileNo);

            var SmsResult = _smsService.FastSend(account.Fullname, order.IssueTrackingNo, account.MobileNo);


            return order.IssueTrackingNo;
        }

        public void SetRefId(int orderId , long refId)
        {
            _orderRepository.SetRefId(orderId, refId);

        }

        public double GetAmountBy(int id)
        {
            return _orderRepository.GetamountBy(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public void Cancel(int id)
        {
            _orderRepository.Cancel(id);
        }

        public List<OrderItemViewModel> GetItems(int orderId)
        {
            return _orderRepository.GetItems(orderId);
        }
    }
}
