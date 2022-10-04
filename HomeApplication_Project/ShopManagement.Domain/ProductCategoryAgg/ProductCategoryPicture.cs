using _0_Framework.Domain.Abstracts;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategoryPicture : Picture
    {
        public ProductCategory ProductCategory { get; set; }

        protected ProductCategoryPicture()
        {

        }
        public ProductCategoryPicture(string path, string alt, string title)
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
