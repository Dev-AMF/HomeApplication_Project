using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Config.Permissions;

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

        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            productCategories = _application.Search(searchModel);    
        }

        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnGetCreate() 
        {
            return Partial("./Create", new CreateProductCategory());
        }

        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnPostCreate(CreateProductCategory command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnGetEdit(int id)
        {
            var productcategory = _application.GetDetails(id);
            return Partial("./Edit", productcategory);
        }

        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnPostEdit(EditProductCategory command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }
    }
}
