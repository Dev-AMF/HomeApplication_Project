using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;

namespace Query.Contracts.Product
{
    public interface IProductQuery
    {
        string GetSlugBy(int id);
        List<ProductQueryModel> GetProducts();
        List<ProductQueryModel> Search(string value);
        List<ProductQueryModel> GetProductsBy(int categoryId);
        List<ProductQueryModel> GetLatestProductsBy(int count);
        ProductQueryModel GetDetails(string slug);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
    