using _0_Framework.Domain;
using CommnetManagement.Infrastructure.EFCore;
using Query.Contracts.Comment;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class CommentQuery : ICommentQuery
    {
        private readonly At_HomeApplicationCommentContext _conetxt;

        public CommentQuery(At_HomeApplicationCommentContext conetxt)
        {
            _conetxt = conetxt;
        }

        

        List<CommentQueryModel> ICommentQuery.GetCommentsByProduct(int id)
        {
            return _conetxt.Comments
                   .Where(C => C.OwnerRecordId == id)
                   .Where(C => C.Status == ApprovalStats.CommentStatus.Approved)
                   .Select(C => new CommentQueryModel
                   {
                       Id = C.Id,
                       Name = C.Name,
                       Message = C.Message

                   })
                   .OrderByDescending( Cqm => Cqm.Id)
                   .ToList();
        }
    }
}
