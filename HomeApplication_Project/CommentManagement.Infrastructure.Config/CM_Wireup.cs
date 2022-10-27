using CommentManagement.Application;
using CommentManagement.Application.Contracts;
using CommentManagement.Domain.CommentAgg;
using CommnetManagement.Infrastructure.EFCore;
using CommnetManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Query.Contracts.Comment;
using Query.Queries;
using System;

namespace CommentManagement.Infrastructure.Config
{
    public static class CM_Wireup
    {
        public static void DoConfig(IServiceCollection services, string ConnectionString)
        {
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddTransient<ICommentQuery, CommentQuery>();

            services.AddDbContext<At_HomeApplicationCommentContext>(
                options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
        }
    }
}
