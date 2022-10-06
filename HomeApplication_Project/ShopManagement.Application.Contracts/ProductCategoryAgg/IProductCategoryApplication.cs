using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(int id);
        List<ProductCategoryViewModel> GetProductCategoryViewModels();
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
