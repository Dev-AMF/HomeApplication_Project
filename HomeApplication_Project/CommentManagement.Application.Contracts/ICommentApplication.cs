using _0_Framework.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Contracts
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Approve(int id);
        OperationResult Disprove(int id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
