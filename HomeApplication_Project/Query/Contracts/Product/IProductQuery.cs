using System.Collections.Generic;

namespace Query.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetProducts();
        List<ProductQueryModel> Search(string value);
        List<ProductQueryModel> GetProductsBy(int categoryId);
        List<ProductQueryModel> GetLatestProductsBy(int count);
    }
}
    