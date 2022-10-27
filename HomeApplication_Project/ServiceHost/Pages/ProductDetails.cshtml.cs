using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentManagement.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;


namespace ServiceHost.Pages
{
    public class ProductDetailsModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductDetailsModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string slug)
        {
            Product = _productQuery.GetDetails(slug);
        }
        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = _0_Framework.Domain.CommentType.Types.Product;

            var result = _commentApplication.Add(command);

            return RedirectToPage("/ProductDetails", productSlug);
        }
    }
}
