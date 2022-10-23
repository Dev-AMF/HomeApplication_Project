using _0_Framework.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductAgg
{
    public class ProductPageMetas : PageMetas
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        protected ProductPageMetas()
        {

        }
        public ProductPageMetas(string keywords, string metaDescription, string slug)
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
