using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFileUploader _uploader;
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository, IFileUploader uploader)
        {
            _repository = repository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var result = new OperationResult();

            if (_repository.Exists( PC => PC.Name == command.Name) )
            {
                return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Name));
            }
            else
            {
                    var picturePath = _uploader.Upload(command.Picture, command.Slug.Slugify());

                    var productcategory = new ProductCategory(command.Name, command.Description, picturePath, command.PictureAlt,
                                                          command.PictureTitle, command.Keywords, command.MetaDescription,
                                                          command.Slug.Slugify());   
                _repository.Save();

                return result.Succeded();
            }
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var result = new OperationResult();
            var prouctCat = _repository.GetIncludings(command.Id);

            if (prouctCat != null)
            {
                if (_repository.Exists(PC => PC.Name == command.Name && PC.Id != command.Id))
                {
                    return result.Failed(String.Format(ApplicationMessage.RecordAlreadyExists, command.Name));
                }
                else
                {
                    if (command.Picture == null)
                    {
                        prouctCat.Edit(command.Name, command.Description, command.PicturePath, command.PictureAlt,
                                                              command.PictureTitle, command.Keywords, command.MetaDescription,
                                                              command.Slug.Slugify());

                    }
                    else
                    {
                        var picturePath = _uploader.Upload(command.Picture, command.Slug.Slugify());

                        prouctCat.Edit(command.Name, command.Description, picturePath, command.PictureAlt,
                                                              command.PictureTitle, command.Keywords, command.MetaDescription,
                                                              command.Slug.Slugify());
                        
                    }
                    _repository.Save();

                    return result.Succeded();
                }
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public EditProductCategory GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategoryViewModels()
        {
            return _repository.GetProductCategoryViewModels();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
