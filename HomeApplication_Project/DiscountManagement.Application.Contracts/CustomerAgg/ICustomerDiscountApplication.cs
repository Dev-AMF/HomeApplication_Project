using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountManagement.Application.Contracts.CustomerAgg
{
    public interface ICustomerDiscountApplication
    {

        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        EditCustomerDiscount GetDetails(int id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
