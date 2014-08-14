using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace SAA.Interfaces.Controllers
{
    public interface IAPIController<T>
    {
        Task<IHttpActionResult> Delete(int id);
        Task<IHttpActionResult> Get(int id);
        Task<IQueryable<T>> Get();
        Task<IHttpActionResult> Post(T item);
        Task<IHttpActionResult> Put(T item);
    }
}
