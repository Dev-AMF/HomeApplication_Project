using Query.Contracts.Orders;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly At_HomeApplicationContext _context;

        public OrderQuery(At_HomeApplicationContext orderContext)
        {
            _context = orderContext;
        }

        public int GetOrdersCountBy(DateTime date, bool orientation)
        {
            if (orientation)
                return _context.Orders.Where(O => O.CreationDate < date).Count();

            return _context.Orders.Where(O => O.CreationDate < date).Count();
        }

        public double GetTotalSale()
        {
            return _context.Orders.Select(O => O.PayAmount).Sum();
        }
    }
}
