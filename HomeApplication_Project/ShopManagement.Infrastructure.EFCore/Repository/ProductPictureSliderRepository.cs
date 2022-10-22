using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureSliderRepository : RepositoryBase<int, ProductPictureSlider>, IProductPictureSliderRepository
    {
        private readonly At_HomeApplicationContext _context;

        public ProductPictureSliderRepository(At_HomeApplicationContext context):base(context)
        {
            _context = context;
        }

        public EditProductPictureSlider GetDetails(int id)
        {
            return _context.ProductsPicturesSliders
                  .Select(PPS => new EditProductPictureSlider
                  {
                   Id = PPS.Id,
                   PicturePath = PPS.Path,
                   Title = PPS.Title,
                   Alt = PPS.Alt,
                   ProductId = PPS.ProductId
                  })
                  .FirstOrDefault(PPS => PPS.Id == id);
        }


        public List<ProductPictureSliderViewModel> Search(ProductPictureSliderSearchModel searchModel)
        {
            var query = 
                   _context.ProductsPicturesSliders
                  .Include(PPS => PPS.Product)
                  .Select(PPS => new ProductPictureSliderViewModel
                  {
                     Id = PPS.Id,
                     PicturePath = PPS.Path,
                     CreationDate = PPS.CreationDate.ToFarsi(),
                     ProductName = PPS.Product.Name,
                     ProductId = PPS.ProductId,
                     IsRemoved = PPS.IsRemoved
                  });
            
            if (searchModel.ProductId != 0)
            {
                query.Where(PPS => PPS.ProductId == searchModel.ProductId);
            }

            return query.ToList();
        }

    }
}
