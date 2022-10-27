using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _uploader;
        private readonly IArticleRepository _repository;
        private readonly IArticleCategoryRepository _categoryRepository;

        public ArticleApplication(IArticleCategoryRepository articleCategoryRepository,
                                   IArticleRepository articleRepository,
                                   IFileUploader fileUploader)
        {
            _uploader = fileUploader;
            _repository = articleRepository;
            _categoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var result = new OperationResult();

            if (_repository.Exists(A => A.Title == command.Title))
            {

                return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Title));
            }
            else
            {

                var articleSlug = command.Slug.Slugify();
                var categorySlug = _categoryRepository.GetSlugBy(command.CategoryId).Slugify();

                var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{articleSlug}", "ArticlePictures");
                var publishDate = command.PublishDate.ToGeorgianDateTime();

                var article = new Article(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureAlt, command.PictureTitle, publishDate, articleSlug, command.Keywords, command.MetaDescription,
                command.CanonicalAddress, command.CategoryId);

                _repository.Create(article);

                _repository.Save();

                return result.Succeded();

            }
        }
        public OperationResult Edit(EditArticle command)
        {
            var result = new OperationResult();
            var article = _repository.Get(command.Id);

            if (article != null)
            {
                if (_repository.Exists(PC => PC.Title == command.Title && PC.Id != command.Id))//اگر داشت نام را برای یک آیدی دیگر ثبت میکرد
                {
                    return result.Failed(String.Format(ApplicationMessages.RecordAlreadyExists, command.Title));
                }
                else
                {
                    if (command.Picture == null)
                    {

                        article.Edit(command.Title, command.ShortDescription, command.Description, article.Picture,
                                     command.PictureAlt, command.PictureTitle, command.PublishDate.ToGeorgianDateTime(),
                                     command.Slug.Slugify(), command.Keywords, command.MetaDescription,
                                     command.CanonicalAddress, command.CategoryId);
                    }
                    else
                    {
                        var articleSlug = command.Slug.Slugify();
                        var categorySlug = _categoryRepository.GetSlugBy(command.CategoryId).Slugify();

                        var picturePath = _uploader.Upload(command.Picture, $"{categorySlug}/{articleSlug}", "ArticlePictures");
                        var publishDate = command.PublishDate.ToGeorgianDateTime();

                        article.Edit(command.Title, command.ShortDescription, command.Description, picturePath,
                                     command.PictureAlt, command.PictureTitle, publishDate, articleSlug, command.Keywords,
                                     command.MetaDescription,command.CanonicalAddress, command.CategoryId);
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

        public EditArticle GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}
