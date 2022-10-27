using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts;
using CommentManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommnetManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly At_HomeApplicationCommentContext _context;

        public CommentRepository(At_HomeApplicationCommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Select(C => new CommentViewModel
                {
                    Id = C.Id,
                    Name = C.Name,
                    Email = C.Email,
                    Website = C.Website,
                    Message = C.Message,
                    OwnerRecordId = C.OwnerRecordId,
                    Type = C.Type,
                    Status = C.Status,
                    CommentDate = C.CreationDate.ToFarsi()
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(C => C.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(C => C.Email.Contains(searchModel.Email));

            return query.OrderByDescending(C => C.Id).ToList();
        }
    }
}
