using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.ProductPictureSlider
{
    public interface IProductPictureSliderQuery
    {
        List<ProductPictureSliderQueryModel> GetPicturesSliderByProduct(int id);
    }
}
