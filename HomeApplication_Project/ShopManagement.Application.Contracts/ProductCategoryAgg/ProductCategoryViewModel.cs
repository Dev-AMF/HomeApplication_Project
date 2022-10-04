namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public string PicturePath { get; set; }
        public int ProductsCount { get; set; }
    }
}
