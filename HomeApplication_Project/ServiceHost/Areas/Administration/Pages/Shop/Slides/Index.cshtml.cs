using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.SlideAgg;


namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        
        public List<SlideViewModel> slides;
        
        private readonly ISlideApplication _application;

        public IndexModel(ISlideApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
            slides = _application.GetList();
        }
        public IActionResult OnGetCreate() 
        {
            var CreatePPS = new CreateSlide();

            return Partial("./Create", CreatePPS);
        }
        public IActionResult OnPostCreate(CreateSlide command)
        {
            var result = _application.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var slide = _application.GetDetails(id);
            return Partial("./Edit", slide);
        }
        public IActionResult OnPostEdit(EditSlide command)
        {
            var result = _application.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(int id)
        {
            _application.Remove(id);

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(int id)
        {
            _application.Restore(id);

            return RedirectToPage("./Index");
        }
    }
}
