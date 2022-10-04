using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var result = new OperationResult();

            if (! _repository.Exists( PC => PC.Name == command.Name) )
            {
                var productcategory = new ProductCategory(command.Name, command.Description, command.PicturePath, command.PictureAlt,
                                                          command.PictureTitle, command.Keywords, command.MetaDescription,
                                                          command.Slug.Slugify());
                _repository.Create(productcategory);
                _repository.Save();

                return result.Succeded();
            }
            else
            {
                return result.Failed($"Another Record With Name-{command.Name}-Already Exists!");
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
                    return result.Failed($"Another Record With Name-{command.Name}-Already Exists!");
                }
                else
                {
                    prouctCat.Edit(command.Name, command.Description, command.PicturePath, command.PictureAlt,
                                                          command.PictureTitle, command.Keywords, command.MetaDescription,
                                                          command.Slug.Slugify());
                    _repository.Save();

                    return result.Succeded();
                }
            }
            else
            {
                return result.Failed($"There Is Not Any Records Matching With The Given Information!");
            }
        }

        public EditProductCategory GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
