using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<Product> Products { get; private set; }

        public ProductCategoryPicture Picture { get; private set; }
        public int PictureId { get; private set; }
        
        public ProductCategoryPageMetas Metas { get; private set; }
        public int MetasId { get; private set; }

        protected ProductCategory()
        {

        }
        public ProductCategory(string name, string descrption, string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = descrption;
            Picture = new ProductCategoryPicture( path,  alt,  title);
            Metas = new ProductCategoryPageMetas(keywords, metaDescription, slug);
            Products = new List<Product>();
        }

        public void Edit(string name, string descrption, string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = descrption;
            Picture.Edit(path, alt, title);
            Metas.Edit(keywords, metaDescription, slug);
        }
    }
}
