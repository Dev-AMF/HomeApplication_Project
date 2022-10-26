using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly At_HomeApplicationBlogContext _context;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryQuery(At_HomeApplicationBlogContext context, IArticleQuery articleQuery)
        {
            _context = context;
            _articleQuery = articleQuery;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Include(AC => AC.Articles)
                .Select(AC => new ArticleCategoryQueryModel
                {
                    Name = AC.Name,
                    Picture = AC.Picture,
                    PictureAlt = AC.PictureAlt,
                    PictureTitle = AC.PictureTitle,
                    Slug = AC.Slug,
                    ArticlesCount = AC.Articles.Count

                }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {

            var articleCategory = _context.ArticleCategories
                .Include(AC => AC.Articles)
                .Select(AC => new ArticleCategoryQueryModel
                {
                    Id = AC.Id,
                    Slug = AC.Slug,
                    Name = AC.Name,
                    Description = AC.Description,
                    Picture = AC.Picture,
                    PictureAlt = AC.PictureAlt,
                    PictureTitle = AC.PictureTitle,
                    Keywords = AC.Keywords,
                    MetaDescription = AC.MetaDescription,
                    CanonicalAddress = AC.CanonicalAddress,
                    ArticlesCount = AC.Articles.Count,
                }).FirstOrDefault(AC => AC.Slug == slug);

            articleCategory.Articles = _articleQuery.GetArticlesByArticleCategory(articleCategory.Id);

            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                articleCategory.KeywordList = articleCategory.Keywords.Split("،").ToList();

            return articleCategory;
        }
    }
}
