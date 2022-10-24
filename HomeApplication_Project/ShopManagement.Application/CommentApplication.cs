using _0_Framework.Application;
using ShopManagement.Application.Contracts.CommentAgg;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public OperationResult Add(AddComment command)
        {

            var operation = new OperationResult();
            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _commentRepository.Create(comment);
            _commentRepository.Save();

            return operation.Succeded();
        }

        public OperationResult Approve(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Approve();
            _commentRepository.Save();

            return operation.Succeded();
        }

        public OperationResult Disprove(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Disprove();
            _commentRepository.Save();

            return operation.Succeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
