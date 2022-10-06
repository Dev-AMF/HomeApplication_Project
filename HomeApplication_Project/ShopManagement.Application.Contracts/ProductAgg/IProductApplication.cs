using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public interface IProductApplication
    {
        OperationResult ActivateInStock(int id);
        OperationResult DeactivateInStock(int id);

        EditProduct GetDetails(int id);

        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        List<ProductViewModel> GetProductViewModels();
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
