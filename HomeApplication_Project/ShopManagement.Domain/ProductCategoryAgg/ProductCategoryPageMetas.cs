using _0_Framework.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategoryPageMetas : PageMetas
    {
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryID { get; set; }
        protected ProductCategoryPageMetas()
        {

        }
        public ProductCategoryPageMetas(string keywords, string metaDescription, string slug)
        {
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string keywords, string metaDescription, string slug)
        {
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
