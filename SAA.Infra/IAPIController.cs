using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.Collections.Generic;

namespace SAA.Infra
{
    public interface IAPIController<T>
    {
        /// <summary>
        /// Retorna todos os registros.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();
        /// <summary>
        /// Retorna um registro especifico baseado no parametro passado.
        /// </summary>
        /// <param name="id">PK da entidade.</param>
        /// <returns></returns>
        Task<T> Get(string id);
        /// <summary>
        /// Insere um novo registro.
        /// </summary>
        /// <param name="item">Entidade a ser adicionada.</param>
        /// <returns></returns>
        Task<IHttpActionResult> Post(T item);
        /// <summary>
        /// Atualiza um novo registro.
        /// </summary>
        /// <param name="item">Entidade a ser atualizada.</param>
        /// <returns></returns>
        Task<IHttpActionResult> Put(T item);
        /// <summary>
        /// Remove um registro existente.
        /// </summary>
        /// <param name="id">PK da entidade</param>
        /// <returns></returns>
        Task<IHttpActionResult> Delete(object id);
    }
}
