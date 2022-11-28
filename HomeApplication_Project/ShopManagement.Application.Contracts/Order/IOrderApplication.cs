using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        void Cancel(int id);
        int PlaceOrder(Cart cart);
        public void SetRefId(int orderId, long refId);
        public double GetAmountBy(int id);
        string PaymentSucceeded(int orderId);
        List<OrderItemViewModel> GetItems(int orderId);
        public List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
