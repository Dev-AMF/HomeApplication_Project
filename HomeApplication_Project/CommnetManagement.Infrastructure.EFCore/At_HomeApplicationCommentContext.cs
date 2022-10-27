using CommentManagement.Domain.CommentAgg;
using CommnetManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommnetManagement.Infrastructure.EFCore
{
    public class At_HomeApplicationCommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public At_HomeApplicationCommentContext(DbContextOptions<At_HomeApplicationCommentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
