using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
