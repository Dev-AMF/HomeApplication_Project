using Query.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Query.Queries
{
    public class CommentQuery : ICommentQuery
    {
        private readonly At_HomeApplicationContext _conetxt;

        public CommentQuery(At_HomeApplicationContext conetxt)
        {
            _conetxt = conetxt;
        }

        

        List<CommentQueryModel> ICommentQuery.GetCommentsByProduct(int id)
        {
            return _conetxt.Comments
                   .Where(C => C.ProductId == id)
                   .Where(C => C.Status == _0_Framework.Domain.ApprovalStats.CommentStatus.Approved)
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
