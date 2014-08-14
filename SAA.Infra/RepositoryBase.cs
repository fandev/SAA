using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace SAA.Infra
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {
        protected DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        public RepositoryBase(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

       // public RepositoryBase() { }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual T GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            if (include.Length == 0)
                return null;
            var todos = AllInclude(include);
            var single = todos.SingleOrDefault(x => x.Id == id);
            return single;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<T> All()
        {
            return _dbSet;
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            var entities = _dbSet.Where(predicate).AsEnumerable<T>();
            _dbSet.RemoveRange(entities);
        }
        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IList<T> AllInclude(params Expression<Func<T, object>>[] include)
        {

           try
            {
                if (include.Length == 0)
                    throw new Exception("Número de parametros inválido.");

                var query = _dbContext.Set<T>().AsQueryable();

                query = include.Aggregate(query, (current, exp) => current.Include(exp));

                IList<T> result = query.ToList();
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.InnerException.Message, ex);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
