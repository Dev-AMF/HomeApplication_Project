using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace _0_Framework.Application
{
    public class FileExtensionValidation : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;
        public FileExtensionValidation(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }


        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtention = Path.GetExtension(file.FileName);
            return _validExtensions.Contains(fileExtention);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtensionValidation", ErrorMessage);
        }
    }
}
