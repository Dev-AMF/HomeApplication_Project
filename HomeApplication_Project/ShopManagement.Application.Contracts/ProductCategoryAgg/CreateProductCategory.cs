using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.ProductCategoryAgg.MetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.ProductCategoryAgg
{
    public partial class CreateProductCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }

    [ModelMetadataType(typeof(CreateProductCategoryMetaData))]
    public partial class CreateProductCategory
    {
    }
}
