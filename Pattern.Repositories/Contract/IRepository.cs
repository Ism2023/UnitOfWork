using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using Pattern.Repositories.Implementation;

namespace Pattern.Repositories.Contract
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Database Database { get; }
        TEntity FindById(object id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void InsertGraph(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        TEntity Detach(TEntity entity);
        RepositoryQuery<TEntity> Query();
    }
}
