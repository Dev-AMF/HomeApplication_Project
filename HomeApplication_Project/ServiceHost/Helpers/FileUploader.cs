using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Helpers
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string folder)
        {
            if (file == null)    
                return "";


            var uploadDirectory = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{folder}";

            if (!Directory.Exists(uploadDirectory)) 
                Directory.CreateDirectory(uploadDirectory);

            var fileName = $"{DateTime.Now.ToFileName()}_{file.FileName}";
            var uploadPath = $"{uploadDirectory}//{fileName}";

            using (var stream = File.Create(uploadPath))
            {
                file.CopyTo(stream);
            }

            
            return $"{folder}/{fileName}";

        }
    }
}
