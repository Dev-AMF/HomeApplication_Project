using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string folder , string parentFolder = "ProductPictures");

        
    }
}
