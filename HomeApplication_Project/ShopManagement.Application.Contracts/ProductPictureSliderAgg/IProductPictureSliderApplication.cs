using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductPictureSliderAgg
{
    public interface IProductPictureSliderApplication
    {
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProductPictureSlider GetDetails(int id);

        OperationResult Create(CreateProductPictureSlider command);
        OperationResult Edit(EditProductPictureSlider command);
        List<ProductPictureSliderViewModel> Search(ProductPictureSliderSearchModel searchModel);

    }
}
