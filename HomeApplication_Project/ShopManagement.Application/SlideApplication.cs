using _0_Framework.Application;
using _0_Framework.Application.Contracts;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _repository;
        private readonly IFileUploader _uploader;

        public SlideApplication(ISlideRepository repository, IFileUploader uploader)
        {
            _repository = repository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var result = new OperationResult();

            var picturePath = _uploader.Upload(command.Picture, "Slides");

            var slide = new Slide(picturePath, command.PictureAlt, command.Title, command.Heading, command.Title,
                                  command.Text, command.ButtonText, command.Link);

            result.Succeded();
            _repository.Create(slide);
            _repository.Save();
            
            return result;
        }

        public OperationResult Edit(EditSlide command)
        {
            var result = new OperationResult();

            var slide = _repository.Get(command.Id);
            
            if (slide != null)
            {
                if (command.Picture == null)
                {
                    slide.Edit(command.PicturePath, command.PictureAlt, command.PictureTitle,
                           command.Heading, command.Title, command.Text,
                           command.ButtonText, command.Link);

                }
                else
                {
                    var picturePath = _uploader.Upload(command.Picture, "Slides");
                    
                    slide.Edit(picturePath, command.PictureAlt, command.PictureTitle,
                           command.Heading, command.Title, command.Text,
                           command.ButtonText, command.Link);
                }

                _repository.Save();
                result.Succeded();

                return result;
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public EditSlide GetDetails(int id)
        {
            return _repository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _repository.GetList();
        }

        public OperationResult Remove(int id)
        {
            var result = new OperationResult();

            var slide = _repository.Get(id);

            if (slide != null)
            {
                slide.Remove();
                _repository.Save();
                result.Succeded();

                return result;
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }

        public OperationResult Restore(int id)
        {
            var result = new OperationResult();

            var slide = _repository.Get(id);

            if (slide != null)
            {
                slide.Restore();
                _repository.Save();
                result.Succeded();

                return result;
            }
            else
            {
                return result.Failed(ApplicationMessage.RecordNotFound);
            }
        }
    }
}
