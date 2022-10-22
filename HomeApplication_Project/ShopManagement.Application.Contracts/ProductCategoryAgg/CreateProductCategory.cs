using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public class CreateProductCategory
    {

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }


        public string Description { get; set; }

        
        [FileExtensionValidation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.FormatIsNotValid)]
        [MaxFileSize(3 * 1024,ErrorMessage = ValidationMessages.FileSizeExceeded)]
        public IFormFile Picture { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }


        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
    }
}
