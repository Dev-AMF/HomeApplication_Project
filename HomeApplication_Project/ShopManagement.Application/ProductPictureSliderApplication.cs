using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureSliderAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class ProductPictureSliderApplication : IProductPictureSliderApplication
    {
        private readonly IProductPictureSliderRepository _repository;

        public ProductPictureSliderApplication(IProductPictureSliderRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProductPictureSlider command)
        {
            var result = new OperationResult();
            
            if (_repository.Exists(PPS => PPS.ProductId == command.ProductId && PPS.Path == command.Path))
            {
                return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Title));
            }
            else
            {
                var productpictureslider = new ProductPictureSlider(command.ProductId, command.Path, command.Alt, command.Title);

                _repository.Create(productpictureslider);
                _repository.Save();

                return result.Succeded();
            }
        }

        public OperationResult Edit(EditProductPictureSlider command)
        {
            var result = new OperationResult();
            var productpictureslider = _repository.Get(command.Id);

            if (productpictureslider != null)
            {
                if (_repository.Exists(PPS => PPS.ProductId == command.ProductId 
                                           && PPS.Path == command.Path 
                                           && PPS.Id != command.Id)) //آپلود تصویر یکسان برای یک محصول
                {
                    return result.Failed(ApplicationMessage.RecordAlreadyExistsNonArgument);
                }
                else
                {
                    productpictureslider.Edit(command.ProductId, command.Path, command.Alt, command.Title);
                    _repository.Save();

                    return result.Succeded();
                }
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
            
        }

        public EditProductPictureSlider GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();
            var productpictureslider = _repository.Get(id);

            if (productpictureslider != null)
            {
                productpictureslider.Deactivate();
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public OperationResult Restore(int id)
        {
            var result = new OperationResult();
            var productpictureslider = _repository.Get(id);

            if (productpictureslider != null)
            {
                productpictureslider.Activate();
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public List<ProductPictureSliderViewModel> Search(ProductPictureSliderSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
