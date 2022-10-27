using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentManagement.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetDisprove(int id)
        {
            var result = _commentApplication.Disprove(id);
            if (result.IsSucceded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetApprove(int id)
        {
            var result = _commentApplication.Approve(id);
            if (result.IsSucceded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

    }
}
