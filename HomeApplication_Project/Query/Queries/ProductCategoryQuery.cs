using Microsoft.EntityFrameworkCore;
using Query.Contracts.Product;
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
        private readonly IProductQuery _productQuery;

        public ProductCategoryQuery(At_HomeApplicationContext context, IProductQuery productQuery)
        {
            _context = context;
            _productQuery = productQuery;
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

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var PCqm =  _context.ProductCategories
                           .Select(PC => new ProductCategoryQueryModel
                           {
                               Id = PC.Id,
                               Name = PC.Name,
                           }).ToList();

            PCqm.ForEach(Pcqm => Pcqm.Products = _productQuery.GetProductsBy(Pcqm.Id));


            return PCqm;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProducstsBy(string slug)
        {
            var PCqm = _context.ProductCategories
                       .Include(PC => PC.Metas)
                       .Select(PC => new ProductCategoryQueryModel
                       {
                           Id = PC.Id,
                           Name = PC.Name,
                           Description = PC.Description,
                           MetaDescription = PC.Metas.MetaDescription,
                           Keywords = PC.Metas.Keywords,
                           Slug = PC.Metas.Slug
                       }).FirstOrDefault(PC => PC.Slug == slug);

            PCqm.Products = _productQuery.GetProductsBy(PCqm.Id);


            return PCqm;
        }
    }
}
