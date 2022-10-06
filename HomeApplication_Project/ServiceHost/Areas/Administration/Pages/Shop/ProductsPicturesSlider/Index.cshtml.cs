using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductsPicturesSlider
{
    public class IndexModel : PageModel
    {
        public ProductPictureSliderSearchModel SearchModel;
        public List<ProductPictureSliderViewModel> productPicturesSliders;
        public SelectList products;

        private readonly IProductApplication _productapplication;
        private readonly IProductPictureSliderApplication _application;

    public IndexModel(IProductApplication productapplication, IProductPictureSliderApplication application)
    {
        _productapplication = productapplication;
        _application = application;
    }

    public void OnGet(ProductPictureSliderSearchModel searchModel)
        {
            products = new SelectList(_productapplication.GetProductViewModels(), "Id", "Name");
            productPicturesSliders = _application.Search(searchModel);   
        }
        public IActionResult OnGetCreate() 
        {
            var CreatePPS = new CreateProductPictureSlider
            {
                ProductViewModels = _productapplication.GetProductViewModels()
            };

            return Partial("./Create", CreatePPS);
        }
        public IActionResult OnPostCreate(CreateProductPictureSlider command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var product = _application.GetDetails(id);
            product.ProductViewModels = _productapplication.GetProductViewModels();

            return Partial("./Edit", product);
        }
        public IActionResult OnPostEdit(EditProductPictureSlider command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(int id)
        {
            _application.Remove(id);

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(int id)
        {
            _application.Restore(id);

            return RedirectToPage("./Index");
        }
    }
}
