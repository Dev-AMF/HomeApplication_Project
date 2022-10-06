using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;

        public ProductApplication(IProductRepository repository)
        {
            _repository = repository;
        }

        public OperationResult ActivateInStock(int id)
        {
            var result = new OperationResult();
            var product = _repository.Get(id);
            
            if (product != null)
            {
                product.ActivateInStock();
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }
        public OperationResult DeactivateInStock(int id)
        {
            var result = new OperationResult();
            var product = _repository.Get(id);

            if (product != null)
            {
                product.DeactivateInStock();
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();

            if (!_repository.Exists(P => P.Name == command.Name))
            {
                var product = new Product(command.Code, command.Name, command.UnitPrice, command.ShortDescription, command.Description, command.CategoryId,
                                          command.PicturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                                          command.Slug.Slugify());
                _repository.Create(product);
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Name));
            }
        }


        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();
            var prouct = _repository.GetIncludings(command.Id);

            if (prouct != null)
            {
                if (_repository.Exists(PC => PC.Name == command.Name && PC.Id != command.Id))//اگر داشت نام را برای یک آیدی دیگر ثبت میکرد
                {
                    return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Name));
                }
                else
                {
                    prouct.Edit(command.Code, command.Name, command.UnitPrice, command.IsInStock, command.ShortDescription, command.Description,
                                command.CategoryId,command.PicturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                                command.Slug.Slugify());
                    
                    _repository.Save();

                    return result.Succeded();
                }
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public EditProduct GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public List<ProductViewModel> GetProductViewModels()
        {
            return _repository.GetProductViewModels();
        }
    }
}
