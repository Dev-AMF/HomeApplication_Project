using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Config.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> products;
        public SelectList productcategories;

        private readonly IProductApplication _application;
        private readonly IProductCategoryApplication _categoryapplication;

        public IndexModel(IProductApplication application, IProductCategoryApplication categoryapplication)
        {
            _application = application;
            _categoryapplication = categoryapplication;
        }

        [NeedsPermission(ShopPermissions.ListProducts)]
        public void OnGet(ProductSearchModel searchModel)
        {
            productcategories = new SelectList(_categoryapplication.GetProductCategoryViewModels(), "Id", "Name");
            products = _application.Search(searchModel);    
        }

        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnGetCreate() 
        {
            var createproduct = new CreateProduct
            {
                Categories = _categoryapplication.GetProductCategoryViewModels()
            };

            return Partial("./Create", createproduct);
        }


        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnPostCreate(CreateProduct command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        
        public IActionResult OnGetEdit(int id)
        {
            var product = _application.GetDetails(id);
            product.Categories = _categoryapplication.GetProductCategoryViewModels();

            return Partial("./Edit", product);
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public IActionResult OnPostEdit(EditProduct command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        
    }
}
