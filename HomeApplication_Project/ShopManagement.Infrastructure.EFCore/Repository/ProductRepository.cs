using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<int, Product>, IProductRepository
    {
        private readonly At_HomeApplicationContext _context;

        public ProductRepository(At_HomeApplicationContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(int id)
        {
            return _context.Products
                //.Include(P => P.Metas)
                //.Include(P => P.Picture)
                .Select(P => new EditProduct
                {
                    Id = P.Id,
                    Name = P.Name,
                    Code = P.Code,
                    Description = P.Description,
                    ShortDescription = P.ShortDescription,
                    CategoryId = P.CategoryId, 
                    Keywords = P.Metas.Keywords,
                    MetaDescription = P.Metas.MetaDescription,
                    Slug = P.Metas.Slug,
                    PicturePath = P.Picture.Path,
                    PictureAlt = P.Picture.Alt,
                    PictureTitle = P.Picture.Title,

                }).FirstOrDefault(EP => EP.Id == id); 
        }

        public Product GetIncludings(int id)
        {
            return _context.Products
                    //.Include(P => P.Picture)
                    //.Include(P => P.Metas)
                    .FirstOrDefault(P => P.Id == id);
        }

        public List<ProductViewModel> GetProductViewModels()
        {
            return _context.Products
                .Select(P => new ProductViewModel
                {
                    Id = P.Id,
                    Name = P.Name,

                }).ToList();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products
                //.Include(P => P.Picture)
                .Select(P => new ProductViewModel
                {
                    Id = P.Id,
                    Name = P.Name,
                    CategoryName = P.Category.Name,
                    CategoryId = P.Category.Id,
                    Code = P.Code,
                    PicturePath = P.Picture.Path,
                    CreationDate = P.CreationDate.ToFarsi()
                });

            if (!String.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(Pvm => Pvm.Name.Contains(searchModel.Name));
            }
            if (!String.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(Pvm => Pvm.Code.Contains(searchModel.Code));
            }
            if (searchModel.CategoryId != 0 )
            {
                query = query.Where(Pvm => Pvm.CategoryId == searchModel.CategoryId);
            }

            return query.ToList();
        }
    }
}
