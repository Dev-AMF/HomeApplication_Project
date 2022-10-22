using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductPictureSliderAgg
{
    public class ProductPictureSliderViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public IFormFile Picture { get; set; }
        public string PicturePath { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
