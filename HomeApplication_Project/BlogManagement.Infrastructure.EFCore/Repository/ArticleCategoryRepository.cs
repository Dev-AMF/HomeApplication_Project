using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<int, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly At_HomeApplicationBlogContext _context;

        public ArticleCategoryRepository(At_HomeApplicationBlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _context.ArticleCategories.Select(AC => new ArticleCategoryViewModel
            {
                Id = AC.Id,
                Name = AC.Name
            }).ToList();
        }

        public EditArticleCategory GetDetails(int id)
        {
            return _context.ArticleCategories.Select(AC => new EditArticleCategory
            {
                Id = AC.Id,
                Name = AC.Name,
                CanonicalAddress = AC.CanonicalAddress,
                Description = AC.Description,
                Keywords = AC.Keywords,
                MetaDescription = AC.MetaDescription,
                ShowOrder = AC.ShowOrder,
                Slug = AC.Slug,
                PictureAlt = AC.PictureAlt,
                PictureTitle = AC.PictureTitle               

            }).FirstOrDefault(AC => AC.Id == id);
        }

        public string GetSlugBy(int id)
        {
            return _context.ArticleCategories
                    .Select(AC => new { AC.Id, AC.Slug })
                    .FirstOrDefault(AC => AC.Id == id)
                    .Slug;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            
            
            var query = _context.ArticleCategories
                .Include(AC => AC.Articles)
                .Select(AC => new ArticleCategoryViewModel
                    {
                        Id = AC.Id,
                        Description = AC.Description,
                        Name = AC.Name,
                        Picture = AC.Picture,
                        ShowOrder = AC.ShowOrder,
                        CreationDate = AC.CreationDate.ToFarsi(),
                        ArticlesCount = AC.Articles.Count()
                    });

            

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(AC => AC.Name.Contains(searchModel.Name));

            return query.OrderByDescending(AC => AC.ShowOrder).ToList();
        }
    }
}
