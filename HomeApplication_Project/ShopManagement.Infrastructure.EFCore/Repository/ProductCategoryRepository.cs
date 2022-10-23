using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<int, ProductCategory> ,  IProductCategoryRepository
    {
        private readonly At_HomeApplicationContext _context;

        public ProductCategoryRepository(At_HomeApplicationContext context) : base(context)
        {
            _context = context;
        }

        
        public EditProductCategory GetDetails(int id)
        {
            return _context.ProductCategories
                //.Include( PC => PC.Metas)
                //.Include(PC => PC.Picture)
                .Select(PC=> new EditProductCategory
                 { 
                    Id = PC.Id,
                    Name = PC.Name,
                    Description = PC.Description,
                    Keywords = PC.Metas.Keywords,
                    MetaDescription = PC.Metas.MetaDescription,
                    Slug = PC.Metas.Slug,
                    PicturePath = PC.Picture.Path,
                    PictureAlt = PC.Picture.Alt,
                    PictureTitle = PC.Picture.Title,

                 }).FirstOrDefault(EPC => EPC.Id == id );
        }


        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories
                //.Include(PC => PC.Picture)
                .Select(PC => new ProductCategoryViewModel
                 {
                     Id = PC.Id,
                     PicturePath = PC.Picture.Path,
                     Name = PC.Name,
                     CreationDate = PC.CreationDate.ToFarsi()
                });
            
            if (! String.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(PCvm => PCvm.Name.Contains(searchModel.Name));
            }

            return query.ToList();
        }

        public ProductCategory GetIncludings(int id)
        {
            return _context.ProductCategories
                    //.Include(Pc => Pc.Picture)
                    //.Include(Pc => Pc.Metas)
                    .FirstOrDefault(Pc => Pc.Id == id);
        }

        public List<ProductCategoryViewModel> GetProductCategoryViewModels()
        {
            return  _context.ProductCategories
                   .Select(PC => new ProductCategoryViewModel
                   {
                       Id = PC.Id,
                       Name = PC.Name,
                   }).ToList();
        }

        public string GetSlugById(int id)
        {
            return _context.ProductCategories
                           .Include(Pc => Pc.Metas)
                           .FirstOrDefault(Pc => Pc.Id == id)
                           .Metas.Slug;
        }
    }
}
