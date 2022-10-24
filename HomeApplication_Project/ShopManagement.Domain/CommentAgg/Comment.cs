using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.CommentAgg
{
    public partial class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public ApprovalStats.CommentStatus Status { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public Comment(string name, string email, string message, int productId)
        {
            Name = name;
            Email = email;
            Message = message;
            ProductId = productId;
            Status = ApprovalStats.CommentStatus.NotSpecified;
        }

        public void Approve()
        {
            Status = ApprovalStats.CommentStatus.Approved;
        }

        public void Disprove()
        {
            Status = ApprovalStats.CommentStatus.Disproved;
        }
    }
}
