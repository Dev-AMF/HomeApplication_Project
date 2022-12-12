using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> Search(string value);
        ArticleQueryModel GetArticleDetails(string slug);
        List<ArticleQueryModel> LatestArticles();
        List<ArticleQueryModel> GetArticlesByArticleCategory(int id);
    }
}
