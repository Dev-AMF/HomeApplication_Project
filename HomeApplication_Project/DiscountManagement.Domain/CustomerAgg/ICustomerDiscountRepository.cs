using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerAgg;

namespace DiscountManagement.Domain.CustomerAgg
{
    public interface ICustomerDiscountRepository : IRepository<int , CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
