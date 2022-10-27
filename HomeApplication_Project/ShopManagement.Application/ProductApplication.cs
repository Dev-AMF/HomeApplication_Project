using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IProductRepository _repository;
        private readonly IFileUploader _uploader;

        public ProductApplication(IProductRepository repository, IFileUploader uploader, IProductCategoryRepository categoryRepository)
        {
            _repository = repository;
            _uploader = uploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var result = new OperationResult();

            if (_repository.Exists(P => P.Name == command.Name))
            {

                return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Name));
            }
            else
            {
                    var categorySlug = _categoryRepository.GetSlugById(command.CategoryId);
                    var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{command.Slug.Slugify()}");

                    var product = new Product(command.Code, command.Name, command.ShortDescription, command.Description, command.CategoryId,
                                          picturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                                          command.Slug.Slugify());
                    _repository.Create(product);

                _repository.Save();

                return result.Succeded();
            }
        }


        public OperationResult Edit(EditProduct command)
        {
            var result = new OperationResult();
            var prouct = _repository.Get(command.Id);

            if (prouct != null)
            {
                if (_repository.Exists(PC => PC.Name == command.Name && PC.Id != command.Id))//اگر داشت نام را برای یک آیدی دیگر ثبت میکرد
                {
                    return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Name));
                }
                else
                {
                    if (command.Picture == null)
                    {

                        prouct.Edit(command.Code, command.Name, command.ShortDescription, command.Description,
                                    command.CategoryId, command.PicturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                                    command.Slug.Slugify());
                    }
                    else
                    {
                        var categorySlug = _categoryRepository.GetSlugById(command.CategoryId);
                        var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{command.Slug.Slugify()}");


                        prouct.Edit(command.Code, command.Name, command.ShortDescription, command.Description,
                                    command.CategoryId, picturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription,
                                    command.Slug.Slugify());
                    }
                    
                    _repository.Save();

                    return result.Succeded();
                }
            }
            else
            {
                return result.Failed(ApplicationMessages.RecordNotFound);
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
