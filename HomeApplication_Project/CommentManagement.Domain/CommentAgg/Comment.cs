using _0_Framework.Domain;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public ApprovalStats.CommentStatus Status { get; private set; }
        public int OwnerRecordId { get; private set; }
        public CommentType.Types Type { get; private set; }
        public int ParentId { get; private set; }
        public Comment Parent { get; private set; }

        public Comment(string name, string email, string website, string message, int ownerRecordId, CommentType.Types type, int parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
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
