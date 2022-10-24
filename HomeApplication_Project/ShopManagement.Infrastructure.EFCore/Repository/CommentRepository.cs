using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.CommentAgg;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly At_HomeApplicationContext _context;
        public CommentRepository(At_HomeApplicationContext context): base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Include(x => x.Product)
                .Select(C => new CommentViewModel
                {
                    Id = C.Id,
                    Email = C.Email,
                    Status = C.Status,
                    Message = C.Message,
                    Name = C.Name,
                    ProductId = C.ProductId,
                    ProductName = C.Product.Name,
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
