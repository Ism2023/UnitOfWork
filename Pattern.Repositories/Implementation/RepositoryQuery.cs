using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pattern.Repositories.Implementation
{
    public sealed class RepositoryQuery<TEntity> where TEntity : class
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
        private readonly List<string> _includePropertiesStrings;
        private readonly Repository<TEntity> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<TEntity, object>>>();
            _includePropertiesStrings = new List<string>();
        }



        public RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public RepositoryQuery<TEntity> Include(string expression)
        {
            _includePropertiesStrings.Add(expression);
            return this;
        }

        public IQueryable<TEntity> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize);
        }

        public IQueryable<TEntity> Get()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize);
        }

        public TEntity Single()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).Single();
        }

        public async Task<TEntity> SingleAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).SingleAsync();
        }
        public async Task<TEntity> SingleOrDefaultAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).SingleOrDefaultAsync();
        }

        public TEntity FirstOrDefault()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).FirstOrDefault();
        }
        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultNoTrackingAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().FirstOrDefaultAsync();
        }

        public int Count()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).Count();
        }
        public async Task<int> CountAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).CountAsync();
        }

        public int CountNoTracking()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().Count();
        }
        public async Task<int> CountNoTrackingAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().CountAsync();
        }

        public IEnumerable<TEntity> ToList()
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).ToList();
        }
        public async Task<List<TEntity>> ToListAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).ToListAsync();
        }
        public async Task<List<TEntity>> ToListNoTrackingAsync()
        {
            return await _repository.Get(_filter, _orderByQuerable, _includeProperties, _includePropertiesStrings, _page, _pageSize).AsNoTracking().ToListAsync();
        }

    }
}
