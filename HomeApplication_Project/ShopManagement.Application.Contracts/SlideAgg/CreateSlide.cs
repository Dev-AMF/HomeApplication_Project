using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class CreateSlide
    {
        [FileExtensionValidation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.FormatIsNotValid)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.FileSizeExceeded)]
        public IFormFile Picture { get; set; }
        public string PicturePath { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Heading { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Text { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ButtonText { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Link { get; set; }
    }
}
