using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountMangement.Infrastructure;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<int, Order>, IOrderRepository
    {
        private readonly At_HomeApplicationContext _context;
        private readonly At_HomeApplicationAccountContext _accountContext;

        public OrderRepository(At_HomeApplicationContext context, At_HomeApplicationAccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetamountBy(int id)
        {
             var order =_context.Orders.Find(id);

            return order == null ? 0 : order.PayAmount;

        }
        public void SetRefId(int orderId, long refId)
        {
            Get(orderId).SetRefId(refId);
            Save();
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(AC => new { AC.Id, AC.Fullname }).ToList();
            var paymentMethods = _context.PaymentMethods.Select(PM => new { PM.Id, PM.Name }).ToList();
            
            var query = _context.Orders.Select(O => new OrderViewModel
            {
                Id = O.Id,
                AccountId = O.AccountId,
                DiscountAmount = O.DiscountAmount,
                IsCanceled = O.IsCanceled,
                IsPaid = O.IsPaid,
                IssueTrackingNo = O.IssueTrackingNo,
                PayAmount = O.PayAmount,
                PaymentMethodId = O.PaymentMethodId,
                RefId = O.RefId,
                TotalAmount = O.TotalAmount,
                CreationDate = O.CreationDate.ToFarsi()
            });

            query = query.Where(O => O.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0) query = query.Where(O => O.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(O => O.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountFullName = accounts.FirstOrDefault(AC => AC.Id == order.AccountId)?.Fullname;
                order.PaymentMethodName = paymentMethods.FirstOrDefault(PM => PM.Id == order.PaymentMethodId)?.Name;
            }

            return orders;
        }

        public void Cancel(int id)
        {
            _context.Orders.Find(id).Cancel();
            Save();
        }

        public List<OrderItemViewModel> GetItems(int orderId)
        {
            var products = _context.Products.Select(p => new { p.Id, p.Name }).ToList();
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(Oi => new OrderItemViewModel
            {
                Id = Oi.Id,
                Count = Oi.Count,
                DiscountRate = Oi.DiscountRate,
                OrderId = Oi.OrderId,
                ProductId = Oi.ProductId,
                UnitPrice = Oi.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.ProductName = products.FirstOrDefault(P => P.Id == item.ProductId)?.Name;
            }

            return items;
        }
    }
}
