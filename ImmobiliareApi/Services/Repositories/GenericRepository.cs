using ImmobiliareApi.DataContext;
using ImmobiliareApi.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ImmobiliareApi.Services.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ImmobiliareApiContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(ImmobiliareApiContext context)
        {
            this._dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async ValueTask<EntityEntry<T>> InsertAsync(T entity)
        {
            var entityDb = await _dbSet.AddAsync(entity);
            return entityDb;
        }
        public virtual Task InsertRangeAsync(IEnumerable<T> entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

          public async Task<T?> FirstOrDefaultAsync(Func<IQueryable<T>, IQueryable<T>>? filterPredicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Expression<Func<T, T>>? selectExpression = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = BaseQuery(filterPredicate, include, enableTracking: enableTracking, ignoreQueryFilters: ignoreQueryFilters, selectExpression: selectExpression);
            return orderBy != null ? await orderBy(query).FirstOrDefaultAsync() : await query.FirstOrDefaultAsync();
        }

        public IQueryable<T> BaseQuery(Func<IQueryable<T>, IQueryable<T>>? filterPredicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Expression<Func<T, T>>? selectExpression = null,
            bool enableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (filterPredicate != null)
            {
                query = filterPredicate(query);
            }

            if (selectExpression != null)
            {
                query = query.Select(selectExpression);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }
            return query;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet;

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate can not be null");
            }

            return await query.AnyAsync(predicate);
        }
        public async Task<List<T>> GetListAsync(Func<IQueryable<T>, IQueryable<T>>? filterPredicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Expression<Func<T, T>>? selectExpression = null,
            bool enableTracking = true)
        {
            IQueryable<T> query = BaseQuery(filterPredicate, include, enableTracking: enableTracking, selectExpression: selectExpression);

            return await (orderBy != null
                ? orderBy(query).ToListAsync() : query.ToListAsync());
        }

        public async Task<IQueryable<T>> GetIQuerableListAsync(Func<IQueryable<T>, IQueryable<T>>? filterPredicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Expression<Func<T, T>>? selectExpression = null,
            bool enableTracking = true)
        {
            IQueryable<T> query = BaseQuery(filterPredicate, include, enableTracking: enableTracking, selectExpression: selectExpression);

            return (orderBy != null ? orderBy(query) : query);
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
               
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
