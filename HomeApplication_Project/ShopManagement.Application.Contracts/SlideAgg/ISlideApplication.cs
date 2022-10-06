using _0_Framework.Application;
using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public interface ISlideApplication 
    {
        OperationResult Create(CreateSlide command); // It's Accessible From Base Repo 
        OperationResult Edit(EditSlide command); // It's Accessible From Domain Class
        OperationResult Remove(int id); // It's Accessible From Domain Class
        OperationResult Restore(int id); // It's Accessible From Domain Class
        EditSlide GetDetails(int id); // Has To Be Implemented In Repo
        List<SlideViewModel> GetList(); // Has To Be Implemented In Repo
    }
}
