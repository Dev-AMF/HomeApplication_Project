using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{
    public class ProductDetailsModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;

        public ProductDetailsModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
       
        public void OnGet(string slug)
        {
            Product = _productQuery.GetDetails(slug);
        }
    }
}
