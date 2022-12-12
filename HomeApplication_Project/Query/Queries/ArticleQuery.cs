using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Query.Contracts.Article;
using Query.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Queries
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly At_HomeApplicationBlogContext _context;
        private readonly ICommentQuery _commentQuery;

        public ArticleQuery(At_HomeApplicationBlogContext context, ICommentQuery commentQuery)
        {
            _context = context;
            _commentQuery = commentQuery;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
               .Include(AC => AC.Category)
               .Where(AC => AC.PublishDate <= DateTime.Now)
               .Select(AC => new ArticleQueryModel
               {
                   Id = AC.Id,
                   CategoryId = AC.CategoryId,
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

            article.Comments = _commentQuery.GetCommentsByArticle(article.Id);

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

        public List<ArticleQueryModel> Search(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                return new List<ArticleQueryModel>();

            var Aqm = _context.Articles
                //.Include(AC => AC.Category)
                .Select(AC => new ArticleQueryModel
                {
                    Title = AC.Title,
                    Slug = AC.Slug,
                    Picture = AC.Picture,
                    PictureAlt = AC.PictureAlt,
                    PictureTitle = AC.PictureTitle,
                    PublishDate = AC.PublishDate.ToFarsi(),
                    ShortDescription = AC.ShortDescription,

                });
                
            
            Aqm = Aqm.Where(Aqm => Aqm.Title.Contains(value) || Aqm.ShortDescription.Contains(value)); 



            //var ListedAqm = Aqm.OrderByDescending(Aqm => Aqm.Id).ToList();

            return Aqm.ToList();
        }
    }
}
