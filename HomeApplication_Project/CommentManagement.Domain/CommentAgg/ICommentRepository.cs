﻿using _0_Framework.Domain;
using CommentManagement.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommentManagement.Domain.CommentAgg
{
    
        public interface ICommentRepository : IRepository<int, Comment>
        {
            List<CommentViewModel> Search(CommentSearchModel searchModel);
        }
    
}
