using _0_Framework.Application;
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

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Path { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Alt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }
        public List<ProductViewModel> ProductViewModels { get; set; }
    }
}
