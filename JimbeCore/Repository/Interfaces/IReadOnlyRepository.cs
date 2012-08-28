using System;
using System.Linq;
using System.Linq.Expressions;

namespace JimbeCore.Repository.Interfaces
{
    public interface IReadOnlyRepository<in TKey, TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity FindBy(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);

        TEntity FindBy(TKey id);
    }
}
