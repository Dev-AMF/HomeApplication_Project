using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.ProductPictureSlider
{
    public class ProductPictureSliderQueryModel
    {
        public int ProductId { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
    }
}
