using System;
using System.Collections.Generic;
using DiscountManagement.Application.Contracts.CustomerAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;


namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> customerDiscounts;
        public SelectList products;

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _application;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication application)
        {
            _productApplication = productApplication;
            _application = application;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            products = new SelectList(_productApplication.GetProductViewModels(), "Id", "Name");
            customerDiscounts = _application.Search(searchModel);    
        }
        public IActionResult OnGetCreate() 
        {
            var createproduct = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProductViewModels()
            };

            return Partial("./Create", createproduct);
        }
        public IActionResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _application.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var customerDiscount = _application.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProductViewModels();

            return Partial("./Edit", customerDiscount);
        }
        public IActionResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        
    }
}
