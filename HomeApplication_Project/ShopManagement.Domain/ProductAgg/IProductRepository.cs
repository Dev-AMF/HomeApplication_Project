using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<int, Product>
    {
        public Product GetIncludings(int id);
        EditProduct GetDetails(int id);

        List<ProductViewModel> GetProductViewModels();
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
