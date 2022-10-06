using _0_Framework.Domain.Abstracts;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.ProductPictureSliderAgg
{
    public class ProductPictureSlider : Picture
    {
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public bool IsRemoved { get; private set; }

        public ProductPictureSlider(int productId, string path, string alt, string title)
        {
            ProductId = productId;
            Path = path;
            Alt = alt;
            Title = title;
            IsRemoved = false;
        }

        public void Edit(int productId, string path, string alt, string title)
        {
            ProductId = productId;
            Path = path;
            Alt = alt;
            Title = title;
        }

        public void Activate()
        {
            IsRemoved = false;
        }
        public void Deactivate()
        {
            IsRemoved = true;
        }
    }
}
