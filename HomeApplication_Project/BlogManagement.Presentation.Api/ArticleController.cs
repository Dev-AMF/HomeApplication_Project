using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Article;
using System;
using System.Collections.Generic;

namespace BlogManagement.Presentation.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleQuery _articleQuery;

        public ArticleController(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        [HttpGet]
        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _articleQuery.LatestArticles();
        }
    }
}
