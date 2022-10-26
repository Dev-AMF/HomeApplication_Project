using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetArticlesByArticleCategory(int id);
        List<ArticleQueryModel> LatestArticles();
        ArticleQueryModel GetArticleDetails(string slug);
    }
}
