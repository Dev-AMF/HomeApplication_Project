using _0_Framework.Domain;
using ShopManagement.Application.Contracts.SlideAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<int, Slide>
    {
        EditSlide GetDetails(int id);
        List<SlideViewModel> GetList();
    }
}
