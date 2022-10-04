using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace _0_Framework.Domain
{
    public interface IRepository<Tkey , TEntity> where TEntity : EntityBase
    {
        void Save();
        void Create(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        TEntity Get(Tkey id);
        List<TEntity> GetAll();
    }
}
