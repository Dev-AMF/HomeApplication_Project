using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Domain.Abstracts
{
    public abstract class Picture : EntityBase
    {
        public string Path { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
    }
}
