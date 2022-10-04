using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Domain.Abstracts
{
    public abstract class PageMetas : EntityBase
    {
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
