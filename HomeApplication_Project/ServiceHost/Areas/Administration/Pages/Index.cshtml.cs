using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Accounts;
using Query.Contracts.Orders;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public int OrdersCount { get; set; }
        public int AccountsCount { get; set; }
        public double TotalSale { get; set; }

        private readonly IOrderQuery _orderQuery;
        private readonly IAccountQuery _accountQuery;

        public IndexModel(IOrderQuery orderQuery, IAccountQuery accountQuery)
        {
            _orderQuery = orderQuery;
            _accountQuery = accountQuery;
        }
        public void OnGet()
        {
            OrdersCount = _orderQuery.GetOrdersCountBy(DateTime.Now, true);
            TotalSale = _orderQuery.GetTotalSale();
            AccountsCount = _accountQuery.GetUsersCountBy(DateTime.Now, true, 3);
        }
    }
}
