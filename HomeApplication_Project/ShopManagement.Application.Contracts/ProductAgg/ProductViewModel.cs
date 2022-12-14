namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public string CreationDate { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
