using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductPictureSliderAgg
{
    public class CreateProductPictureSlider 
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }

        [FileExtensionValidation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.FormatIsNotValid)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.FileSizeExceeded)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PicturePath { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Alt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }
        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
