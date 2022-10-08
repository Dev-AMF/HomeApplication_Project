using Query.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class SlideQuery : ISlideQuery
    {
        private readonly At_HomeApplicationContext _conetxt;

        public SlideQuery(At_HomeApplicationContext conetxt)
        {
            _conetxt = conetxt;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _conetxt.Sliders.Where(S => S.IsRemoved == false)
                            .Select(S => new SlideQueryModel
                            {
                                Picture = S.Picture,
                                PictureAlt = S.PictureAlt,
                                PictureTitle = S.PictureTitle,
                                ButtonText = S.ButtonText,
                                Heading = S.Heading,
                                Link = S.Link,
                                Text = S.Text,
                                Title = S.Title
                            }).ToList();
        }
    }
}
