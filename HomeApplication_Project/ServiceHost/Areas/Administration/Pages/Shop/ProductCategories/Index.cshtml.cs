using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> productCategories;

        private readonly IProductCategoryApplication _application;

        public IndexModel(IProductCategoryApplication application)
        {
            _application = application;
        }


        public void OnGet(ProductCategorySearchModel searchModel)
        {
            productCategories = _application.Search(searchModel);    
        }
        public IActionResult OnGetCreate() 
        {
            return Partial("./Create", new CreateProductCategory());
        }
        public IActionResult OnPostCreate(CreateProductCategory command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var productcategory = _application.GetDetails(id);
            return Partial("./Edit", productcategory);
        }
        public IActionResult OnPostEdit(EditProductCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
