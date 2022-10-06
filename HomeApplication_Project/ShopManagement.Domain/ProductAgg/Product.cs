using _0_Framework.Domain;
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
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        
        public int CategoryId { get; private set; }
        public ProductCategory Category { get; set; }

        public ProductPicture Picture { get; private set; }
        public int PictureId { get; private set; }

        public ProductPageMetas Metas { get; private set; }
        public int MetasId { get; private set; }

        public ICollection<ProductPictureSlider> ProductPicturesSlider { get; private set; }


        protected Product()
        {

        }

        public Product(string code, string name, double unitPrice, string shortDescription, string description, int categoryId, 
                       string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Code = code;
            Name = name;
            UnitPrice = unitPrice;
            ActivateInStock () ;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            Picture = new ProductPicture(path, alt, title);
            Metas = new ProductPageMetas(keywords, metaDescription, slug); ;
        }

        public void Edit(string code, string name, double unitPrice, bool isInStock, string shortDescription, string description, int categoryId,
                         string path, string alt, string title, string keywords, string metaDescription, string slug)
        {
            Code = code;
            Name = name;
            UnitPrice = unitPrice;
            IsInStock = isInStock ;
            ShortDescription = shortDescription;
            Description = description;
            CategoryId = categoryId;
            Picture.Edit(path, alt, title);
            Metas.Edit (keywords, metaDescription, slug); ;
        }

        public void ActivateInStock ()
        {
            this.IsInStock = true;
        }
        public void DeactivateInStock ()
        {
            this.IsInStock = false;
        }
    }
}
