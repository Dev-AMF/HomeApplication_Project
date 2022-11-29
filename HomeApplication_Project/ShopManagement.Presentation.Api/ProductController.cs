using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Product;
using System;
using System.Collections.Generic;

namespace ShopManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            this._productQuery = productQuery;
        }

        [HttpGet]
        public List<ProductQueryModel> GetProducts()
        {
            return _productQuery.GetProducts();
        }
    }
}
