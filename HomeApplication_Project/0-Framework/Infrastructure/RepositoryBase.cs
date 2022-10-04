using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<Tkey, TEntity> : IRepository<Tkey, TEntity> where TEntity : EntityBase
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public void Create(TEntity entity)
        {
            _context.Add<TEntity>(entity);
            Save();
        }

        public bool Exists(Expression<Func<TEntity , bool>> expression)
        {
            return _context.Set<TEntity>().Any(expression);
        }

        public TEntity Get(Tkey id)
        {
            return _context.Find<TEntity>(id);
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
