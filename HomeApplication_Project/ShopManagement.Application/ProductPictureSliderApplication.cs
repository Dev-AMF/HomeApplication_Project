using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class ProductPictureSliderApplication : IProductPictureSliderApplication
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductPictureSliderRepository _pictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _uploader;

        public ProductPictureSliderApplication(IProductCategoryRepository categoryRepository, 
                                               IProductPictureSliderRepository pictureRepository, 
                                               IProductRepository productRepository, 
                                               IFileUploader uploader)
        {
            _categoryRepository = categoryRepository;
            _pictureRepository = pictureRepository;
            _productRepository = productRepository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateProductPictureSlider command)
        {
            var result = new OperationResult();

            //if (_repository.Exists(PPS => PPS.ProductId == command.ProductId && PPS.Path == command.PicturePath))
            //{
            //    return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Title));
            //}

            var product = _productRepository.Get(command.ProductId);

            var productSlug = _productRepository.Get(product.Id).Metas.Slug;
            var categorySlug = _categoryRepository.Get(product.CategoryId).Metas.Slug;

            var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{productSlug}");

            var productpictureslider = new ProductPictureSlider(command.ProductId, picturePath, command.Alt, command.Title);

                _pictureRepository.Create(productpictureslider);
                _pictureRepository.Save();

                return result.Succeded();
            
        }

        public OperationResult Edit(EditProductPictureSlider command)
        {
            var result = new OperationResult();
            var productpictureslider = _pictureRepository.Get(command.Id);

            if (productpictureslider != null)
            {
                //if (_repository.Exists(PPS => PPS.ProductId == command.ProductId 
                //                           && PPS.Path == command.PicturePath 
                //                           && PPS.Id != command.Id)) //آپلود تصویر یکسان برای یک محصول
                //{
                //    return result.Failed(ApplicationMessage.RecordAlreadyExistsNonArgument);
                //}
                    if (command.Picture == null)
                    {
                        productpictureslider.Edit(command.ProductId, command.PicturePath, command.Alt, command.Title);
                    }
                    else
                    {
                    var product = _productRepository.Get(command.ProductId);

                    var productSlug = _productRepository.Get(product.Id).Metas.Slug;
                    var categorySlug = _categoryRepository.Get(product.CategoryId).Metas.Slug;

                    var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{productSlug}");

                    productpictureslider.Edit(command.ProductId, picturePath, command.Alt, command.Title);
                    }
                    
                    _pictureRepository.Save();

                    return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
            
        }

        public EditProductPictureSlider GetDetails(int id)
        {
            return _pictureRepository.GetDetails(id);
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            var productpictureslider = _pictureRepository.Get(id);

            if (productpictureslider != null)
            {
                productpictureslider.Deactivate();
                _pictureRepository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
        }

        public OperationResult Restore(int id)
        {
            var result = new OperationResult();
            var productpictureslider = _pictureRepository.Get(id);

            if (productpictureslider != null)
            {
                productpictureslider.Activate();
                _pictureRepository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
            }
        }

        public List<ProductPictureSliderViewModel> Search(ProductPictureSliderSearchModel searchModel)
        {
            return _pictureRepository.Search(searchModel);
        }
    }
}
