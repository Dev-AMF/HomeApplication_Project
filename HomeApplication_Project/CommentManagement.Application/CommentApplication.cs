using _0_Framework.Application;
using CommentManagement.Application.Contracts;
using CommentManagement.Domain.CommentAgg;
using System.Collections.Generic;

namespace CommentManagement.Application
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

            var comment = new Comment(command.Name, command.Email, command.Website, command.Message,
                command.OwnerRecordId, command.Type, command.ParentId);

            _commentRepository.Create(comment);
            _commentRepository.Save();
            
            return operation.Succeded();
        }

        public OperationResult Approve(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Approve();
            _commentRepository.Save();

            return operation.Succeded();
        }

        public OperationResult Disprove(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            
            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

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
