using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;

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


        public void OnGet(ProductSearchModel searchModel)
        {
            productcategories = new SelectList(_categoryapplication.GetProductCategoryViewModels(), "Id", "Name");
            products = _application.Search(searchModel);    
        }
        public IActionResult OnGetCreate() 
        {
            var createproduct = new CreateProduct
            {
                Categories = _categoryapplication.GetProductCategoryViewModels()
            };

            return Partial("./Create", createproduct);
        }
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
        public IActionResult OnPostEdit(EditProduct command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDeactivateInStock(int id)
        {
            _application.DeactivateInStock(id);

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetActivateInStock(int id)
        {
            _application.ActivateInStock(id);

            return RedirectToPage("./Index");
        }
    }
}
