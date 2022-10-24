using _0_Framework.Domain;

namespace ShopManagement.Application.Contracts.CommentAgg
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ApprovalStats.CommentStatus Status { get; set; }
        public string CommentDate { get; set; }
    }
}
