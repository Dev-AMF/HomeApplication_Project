﻿using _0_Framework.Domain;

namespace CommentManagement.Application.Contracts
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Website { get; set; }
        public int OwnerRecordId { get; set; }
        public CommentType.Types Type { get; set; }
        public int ParentId { get; set; }
    }
}
