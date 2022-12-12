using Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Query.Contracts.Article;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        
        public List<ProductQueryModel> Products;
        public List<ArticleQueryModel> Articles;
        
        private readonly IProductQuery _productQuery;
        public readonly IArticleQuery _articleQuery;

        public SearchModel(IProductQuery productQuery, IArticleQuery articleQuery)
        {
            _productQuery = productQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);
            Articles = _articleQuery.Search(value);
        }

        public void OnPost(string value)
        {
            Value = value;
            Products = _productQuery.Search(value);
            Articles = _articleQuery.Search(value);
        }
    }
}
