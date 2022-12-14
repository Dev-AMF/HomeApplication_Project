using System.Collections.Generic;

namespace Query.Contracts.Comment
{
    public interface ICommentQuery 
    {
        public List<CommentQueryModel> GetCommentsByProduct(int id);
        public List<CommentQueryModel> GetCommentsByArticle(int id);
    }
}
