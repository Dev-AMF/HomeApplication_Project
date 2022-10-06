using _0_Framework.Application;
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

        public SlideApplication(ISlideRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var result = new OperationResult();
            
            var slide = new Slide(command.Picture, command.PictureAlt, command.Title, command.Heading, command.Title,
                                  command.Text, command.ButtonText);

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
                slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, 
                           command.Heading, command.Title, command.Text, 
                           command.ButtonText);
                
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
