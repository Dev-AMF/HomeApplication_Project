﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Query.Contracts.Comment
{
    public class CommentQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}