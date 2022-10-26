using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<int, Article>, IArticleRepository
    {
        private readonly At_HomeApplicationBlogContext _context;

        public ArticleRepository(At_HomeApplicationBlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(int id)
        {
            return _context.Articles.Select(A => new EditArticle
            {
                Id = A.Id,
                CanonicalAddress = A.CanonicalAddress,
                CategoryId = A.CategoryId,
                Description = A.Description,
                Keywords = A.Keywords,
                MetaDescription = A.MetaDescription,
                PictureAlt = A.PictureAlt,
                PictureTitle = A.PictureTitle,
                PublishDate = A.PublishDate.ToFarsi(),
                ShortDescription = A.ShortDescription,
                Slug = A.Slug,
                Title = A.Title

            }).FirstOrDefault(A => A.Id == id);
        }

        public Article GetWithCategory(int id)
        {
            return _context.Articles
                           .Include(A => A.Category)
                           .FirstOrDefault(A => A.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(A => new ArticleViewModel
            {
                Id = A.Id,
                Category = A.Category.Name,
                Picture = A.Picture,
                PublishDate = A.PublishDate.ToFarsi(),
                ShortDescription = A.ShortDescription.Substring(0, Math.Min(A.ShortDescription.Length, 50)) + " ...",
                Title = A.Title
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(A => A.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(A => A.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(A => A.Id)
                        .ToList();
        }
    }
}
