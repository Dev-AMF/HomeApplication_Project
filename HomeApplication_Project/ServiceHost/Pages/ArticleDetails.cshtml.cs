using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentManagement.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;

namespace ServiceHost.Pages
{
    public class ArticleDetailsModel : PageModel
    {

        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _query;
        private readonly ICommentApplication _commentApplication;

        public ArticleDetailsModel(IArticleQuery articleQuery, IArticleCategoryQuery query, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _query = query;
            _commentApplication = commentApplication;
        }

        public void OnGet(string slug)
        {
            Article = _articleQuery.GetArticleDetails(slug);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _query.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {

            command.Type = _0_Framework.Domain.CommentType.Types.Article;
            var result = _commentApplication.Add(command);

            return RedirectToPage("/ArticleDetails", new { slug = articleSlug });
        }
    }
}
