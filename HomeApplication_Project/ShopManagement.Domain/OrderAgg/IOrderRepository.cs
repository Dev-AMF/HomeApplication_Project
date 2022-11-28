using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<int, Order>
    {
        void Cancel(int id);
        double GetamountBy(int id);
        public void SetRefId(int orderId, long refId);
        List<OrderItemViewModel> GetItems(int orderId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
