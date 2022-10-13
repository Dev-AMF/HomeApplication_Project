using Microsoft.EntityFrameworkCore;
using Query.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly At_HomeApplicationContext _context;

        public ProductCategoryQuery(At_HomeApplicationContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories
                           .Include(PC => PC.Picture)
                           .Include(PC => PC.Metas)
                           .Select(PC => new ProductCategoryQueryModel 
                           {
                               Id = PC.Id,
                               Name = PC.Name,
                               Picture = PC.Picture.Path,
                               PictureAlt = PC.Picture.Alt,
                               PictureTitle = PC.Picture.Title,
                               Slug = PC.Metas.Slug

                           }).ToList();
        }
    }
}
