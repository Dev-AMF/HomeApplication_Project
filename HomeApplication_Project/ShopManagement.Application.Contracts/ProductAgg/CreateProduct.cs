using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }

        public bool IsInStock { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }
        
        public string Description { get; set; }
        
        [Range(0,100,ErrorMessage = ValidationMessages.IsRequired)]
        public int CategoryId { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
          
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
