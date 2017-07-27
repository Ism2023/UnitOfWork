using Pattern.DataContext.Contract;
using Pattern.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IDbContext _context;
        private IDbSet<TEntity> _dbSet;

        public Database Database
        {
            get
            {
                return _context.Database;
            }
        }

        public Repository(IDbContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }

        public virtual TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet.Where(predicate);
            return query;
        }

        public virtual void InsertGraph(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.ContextEntry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);

            _context.ContextEntry(entity).State = EntityState.Deleted;

            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.ContextEntry(entity).State = EntityState.Added;
        }

        public virtual TEntity Detach(TEntity entity)
        {
            _context.ContextEntry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         List<Expression<Func<TEntity, object>>> includeProperties = null,
                                         List<string> includePropertiesString = null,
                                         int? page = null,
                                         int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (includePropertiesString != null)
                includePropertiesString.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }

    }
}
