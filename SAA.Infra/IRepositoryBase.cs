using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAA.Infra
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetByIdAsync(int id);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);

        IQueryable<T> All();

        void Edit(T entity);

        void Insert(T entity);

        void DeleteAsync(T entity);

        IList<T> AllInclude(params Expression<Func<T, object>>[] include);
    }
}
