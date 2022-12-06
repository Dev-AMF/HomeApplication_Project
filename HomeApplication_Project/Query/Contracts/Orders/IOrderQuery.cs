using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Orders
{
    public interface IOrderQuery
    {
        public int GetOrdersCountBy(DateTime date, bool orientation);
        public double GetTotalSale();
    }
}
