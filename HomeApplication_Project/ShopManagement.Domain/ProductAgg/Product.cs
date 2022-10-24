using _0_Framework.Domain;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        
        public int CategoryId { get; private set; }
        public ProductCategory Category { get; set; }

        public ProductPicture Picture { get; private set; }
        

        public ProductPageMetas Metas { get; private set; }
        

        public ICollection<ProductPictureSlider> ProductPicturesSlider { get; private set; }
        public ICollection<Comment> Comments { get; private set; }

        protected Product()
        {

        }

        public Product(string code, string name, string shortDescription, string description, int categoryId, 
                       string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Code = code;
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            Picture = new ProductPicture(path, alt, title);
            Metas = new ProductPageMetas(keywords, metaDescription, slug); ;
        }

        public void Edit(string code, string name, string shortDescription, string description, int categoryId,
                         string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Code = code;
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            Picture.Edit(path, alt, title);
            Metas.Edit (keywords, metaDescription, slug); ;
        }

        
    }
}
