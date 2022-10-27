using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {

        private readonly IFileUploader _uploader;
        //private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _repository;

        public ArticleCategoryApplication(IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _uploader = fileUploader;
            _repository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var result = new OperationResult();

            if (_repository.Exists(A => A.Name == command.Name))
            {

                return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Name));
            }
            else
            {
                var categorySlug = command.Slug.Slugify();
                var picturePath = _uploader.Upload(command.Picture, command.Slug.Slugify(), "ArticlePictures");

                var articleCategory = new ArticleCategory(command.Name, picturePath, command.PictureAlt, command.PictureTitle,
                                                          command.Description, command.ShowOrder, categorySlug, command.Keywords, 
                                                          command.MetaDescription, command.CanonicalAddress);

                _repository.Create(articleCategory);

                _repository.Save();

                return result.Succeded();
            }
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var result = new OperationResult();
            var articleCategory = _repository.Get(command.Id);

            if (articleCategory != null)
            {
                if (_repository.Exists(PC => PC.Name == command.Name && PC.Id != command.Id))//اگر داشت نام را برای یک آیدی دیگر ثبت میکرد
                {
                    return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Name));
                }
                else
                {
                    if (command.Picture == null)
                    {

                        articleCategory.Edit(command.Name, articleCategory.Picture, command.PictureAlt, command.PictureTitle,
                                             command.Description, command.ShowOrder, command.Slug.Slugify(),
                                             command.Keywords, command.MetaDescription, command.CanonicalAddress);
                    }
                    else
                    {
                        var categorySlug = command.Slug.Slugify();
                        var picturePath = _uploader.Upload(command.Picture, command.Slug.Slugify(), "ArticlePictures");
                        

                        articleCategory.Edit(command.Name, picturePath, command.PictureAlt, command.PictureTitle,
                                             command.Description, command.ShowOrder, categorySlug,
                                             command.Keywords, command.MetaDescription, command.CanonicalAddress);
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

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _repository.GetArticleCategories(); ;
        }

        public EditArticleCategory GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
