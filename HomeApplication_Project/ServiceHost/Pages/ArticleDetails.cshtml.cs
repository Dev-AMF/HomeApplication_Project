using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ArticleDetailsModel(IArticleQuery articleQuery, IArticleCategoryQuery query)
        {
            _articleQuery = articleQuery;
            _query = query;
        }

        public void OnGet(string slug)
        {

            Article = _articleQuery.GetArticleDetails(slug);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _query.GetArticleCategories();
        }
    }
}
