using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductPictureSliderAgg
{
    public interface IProductPictureSliderRepository : IRepository<int , ProductPictureSlider>
    {
        EditProductPictureSlider GetDetails(int id);
        List<ProductPictureSliderViewModel> Search(ProductPictureSliderSearchModel searchModel);    
    }
}
