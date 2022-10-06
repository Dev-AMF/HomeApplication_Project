using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<int, Slide>, ISlideRepository
    {
        private readonly At_HomeApplicationContext _context;

        public SlideRepository(At_HomeApplicationContext context):base (context)
        {
            _context = context;
        }

        public EditSlide GetDetails(int id)
        {
            return _context.Sliders
                           .Select(S => new EditSlide
                           {
                               Id = S.Id,
                               ButtonText = S.ButtonText,
                               Heading = S.Heading,
                               Picture = S.Picture,
                               PictureAlt = S.PictureAlt,
                               PictureTitle = S.PictureTitle,
                               Text = S.Text,
                               Title = S.Title

                           }).FirstOrDefault(S => S.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Sliders.Select(S => new SlideViewModel
            {
                Id = S.Id,
                Heading = S.Heading,
                Picture = S.Picture,
                Title = S.Title,
                IsRemoved = S.IsRemoved,
                CreationDate = S.CreationDate.ToString()

            }).ToList();
        }
    }
}
