using Query.Contracts.ProductPictureSlider;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class ProductPictureSliderQuery : IProductPictureSliderQuery
    {
        private readonly At_HomeApplicationContext _context;

        public ProductPictureSliderQuery(At_HomeApplicationContext context)
        {
            _context = context;
        }

        public List<ProductPictureSliderQueryModel> GetPicturesSliderByProduct(int id)
        {
            return _context.ProductsPicturesSliders
                           .Select(PPS => new ProductPictureSliderQueryModel 
                           {
                               IsRemoved = PPS.IsRemoved,
                               PicturePath = PPS.Path,
                               PictureAlt = PPS.Alt,
                               PictureTitle = PPS.Title,
                               ProductId = PPS.ProductId

                           })
                           .Where(PPS => PPS.IsRemoved == false)
                           .ToList();
        }
    }
}
