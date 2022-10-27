using _0_Framework.Domain;

namespace CommentManagement.Application.Contracts
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public int OwnerRecordId { get; set; }
        public string OwnerName { get; set; }
        public CommentType.Types Type { get; set; }
        public ApprovalStats.CommentStatus Status { get; set; }
        public string CommentDate { get; set; }
    }
}
