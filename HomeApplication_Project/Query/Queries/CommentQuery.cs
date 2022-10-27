using _0_Framework.Application;
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

        public List<CommentQueryModel> GetCommentsByArticle(int id)
        {

         var comments =  _conetxt.Comments
                         .Where(C => C.OwnerRecordId == id)
                         .Where(C => C.Status == ApprovalStats.CommentStatus.Approved)
                         .Where(C => C.Type == CommentType.Types.Article)
                         .Select(C => new CommentQueryModel
                         {
                             Id = C.Id,
                             Message = C.Message,
                             Name = C.Name,
                             ParentId = C.ParentId,
                             CreationDate = C.CreationDate.ToFarsi()

                         })
                         .OrderByDescending(Cqm => Cqm.Id)
                         .ToList();
            

            foreach (var item in comments)
            {
                if (item.ParentId > 0)
                    item.ParentName = comments.FirstOrDefault(C => C.Id == item.ParentId)?.Name;
            }

            return comments;
        }

        public List<CommentQueryModel> GetCommentsByProduct(int id)
        {
                return _conetxt.Comments
                       .Where(C => C.OwnerRecordId == id)
                       .Where(C => C.Status == ApprovalStats.CommentStatus.Approved)
                       .Where(C => C.Type == CommentType.Types.Product )
                       .Select(C => new CommentQueryModel
                       {
                           Id = C.Id,
                           Name = C.Name,
                           Message = C.Message

                       })
                       .OrderByDescending(Cqm => Cqm.Id)
                       .ToList();
        }
    }
}
