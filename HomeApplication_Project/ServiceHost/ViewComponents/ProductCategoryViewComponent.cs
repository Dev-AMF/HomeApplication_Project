using Microsoft.AspNetCore.Mvc;
using Query.Contracts.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public ProductCategoryViewComponent(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke() 
        {
            var productcategories = _query.GetProductCategories();
            
            return View(productcategories);
        }
    }
}
