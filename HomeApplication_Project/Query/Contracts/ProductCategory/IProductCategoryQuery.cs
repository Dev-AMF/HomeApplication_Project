using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
        ProductCategoryQueryModel GetProductCategoryWithProducstsBy(string slug);
    }
}
