using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<int, ProductCategory>
    {
        public ProductCategory GetIncludings(int id);
        EditProductCategory GetDetails(int id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
