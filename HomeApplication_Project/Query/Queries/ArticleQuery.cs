using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly At_HomeApplicationBlogContext _context;

        public ArticleQuery(At_HomeApplicationBlogContext context)
        {
            _context = context;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
               .Include(AC => AC.Category)
               .Where(AC => AC.PublishDate <= DateTime.Now)
               .Select(AC => new ArticleQueryModel
               {
                   Title = AC.Title,
                   CategoryName = AC.Category.Name,
                   CategorySlug = AC.Category.Slug,
                   Slug = AC.Slug,
                   CanonicalAddress = AC.CanonicalAddress,
                   Description = AC.Description,
                   Keywords = AC.Keywords,
                   MetaDescription = AC.MetaDescription,
                   Picture = AC.Picture,
                   PictureAlt = AC.PictureAlt,
                   PictureTitle = AC.PictureTitle,
                   PublishDate = AC.PublishDate.ToFarsi(),
                   ShortDescription = AC.ShortDescription

               }).FirstOrDefault(AC => AC.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split("،").ToList();

            return article;
        }

        public List<ArticleQueryModel> GetArticlesByArticleCategory(int id)
        {
            var article = 
                _context.Articles
               .Where(A => A.PublishDate <= DateTime.Now)
               .Include(A => A.Category)
               .Select(A => new ArticleQueryModel
               {
                   Id = A.Id,
                   CategoryId = A.CategoryId,
                   Title = A.Title,
                   CategoryName = A.Category.Name,
                   CategorySlug = A.Category.Slug,
                   Slug = A.Slug,
                   CanonicalAddress = A.CanonicalAddress,
                   Description = A.Description,
                   Keywords = A.Keywords,
                   MetaDescription = A.MetaDescription,
                   Picture = A.Picture,
                   PictureAlt = A.PictureAlt,
                   PictureTitle = A.PictureTitle,
                   PublishDate = A.PublishDate.ToFarsi(),
                   ShortDescription = A.ShortDescription

               }).Where(A => A.CategoryId == id)
               .ToList();

            return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles
                .Include(AC => AC.Category)
                .Where(AC => AC.PublishDate <= DateTime.Now)
                .Select(AC => new ArticleQueryModel
                {
                    Title = AC.Title,
                    Slug = AC.Slug,
                    Picture = AC.Picture,
                    PictureAlt = AC.PictureAlt,
                    PictureTitle = AC.PictureTitle,
                    PublishDate = AC.PublishDate.ToFarsi(),
                    ShortDescription = AC.ShortDescription,

                }).ToList();
        }
    }
}
