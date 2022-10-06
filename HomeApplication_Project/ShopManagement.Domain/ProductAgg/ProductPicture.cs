using _0_Framework.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductAgg
{
    public class ProductPicture : Picture
    {
        public Product Product { get; set; }

        protected ProductPicture()
        {

        }
        public ProductPicture(string path, string alt, string title)
        {
            Path = path;
            Alt = alt;
            Title = title;
        }

        public void Edit(string path, string alt, string title)
        {
            Path = path;
            Alt = alt;
            Title = title;
        }
    }
}
