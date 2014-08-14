using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SAA.Infra
{

    /// <summary>
    /// Interface padrão para manipulação de dados.
    /// </summary>
    /// <typeparam name="TSource">Tipo da entidade a ser manipulada. Ex: Empresas</typeparam>
    public interface IRepository<TSource> where TSource : class
    {
        /// <summary>
        /// Retorna um objeto da entidade manipulada conforme o valor do ID.
        /// </summary>
        /// <param name="where">Valores referente ao ID da entidade.</param>
        /// <returns>Entidade encontrada. Caso contrário retorna null.</returns>
        TSource GetByID(Expression<Func<TSource, bool>> where);

        /// <summary>
        /// Retorna um objeto da entidade manipulada conforme o valor do ID.
        /// </summary>
        /// <param name="expand">Lista de propriedades para expansão.</param>
        /// <param name="where">Valores referente ao ID da entidade.</param>
        /// <returns>Entidade encontrada. Caso contrário retorna null.</returns>
        TSource GetByID(string expand, Expression<Func<TSource, bool>> where);


        /// <summary>
        /// Retorna todos objetos da entidade manipulada.
        /// </summary>
        /// <returns>Retorna uma lista dos objetos.</returns>
        IList<TSource> GetAll();

        /// <summary>
        /// Retorna todos objetos da entidade manipulada definindo as propriedade para expansão.
        /// </summary>
        /// <param name="expand">Lista de propriedades para expansão.</param>
        /// <returns>Retorna uma lista dos objetos.</returns>
        IList<TSource> GetAll(string expand);

        /// <summary>
        /// Retorna todos objetos da entidade manipulada definindo as propriedade para expansão.
        /// </summary>
        /// <param name="expand">Lista de propriedades para expansão.</param>
        /// <returns>Retorna uma lista dos objetos.</returns>
        IList<TSource> GetAll(params Expression<Func<TSource, object>>[] expand);

        /// <summary>
        /// Retorna os objetos da entidade manipulada de acordo a condição informada.
        /// </summary>
        /// <param name="predicate">Representa a condição a ser testada pela entidade</param>
        /// <returns>Retorna uma lista dos objetos</returns>
        IList<TSource> GetWhere(Expression<Func<TSource, bool>> predicate);

        /// <summary>
        /// Retorna os objetos da entidade manipulada de acordo a condição informada.
        /// </summary>
        /// <param name="expand">Lista de propriedades para expansão.</param>
        /// <param name="predicate">Representa a condição a ser testada pela entidade</param>
        /// <returns>Retorna uma lista dos objetos</returns>
        IList<TSource> GetWhere(string expand, Expression<Func<TSource, bool>> predicate);

        /// <summary>
        /// Adiciona a entidade indicada.
        /// </summary>
        /// <param name="entity">Entidade a ser incluída.</param>
        /// <returns>Retorna a entidade após a inclusão. Apresentando algum erro retorna null.</returns>
        TSource Add(TSource entity);

        /// <summary>
        /// Exclui a entidade indicada.
        /// </summary>
        /// <param name="entity">Entidade a ser deletada.</param>
        /// <returns>Retorna true se a entidade for excluido. Caso contrário retorna false.</returns>
        bool Delete(TSource entity);

        /// <summary>
        /// Atualiza a entidade indicada.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada.</param>
        /// <returns>Retorna true se a entidade for atualizado. Caso contrário retorna false.</returns>
        bool Update(TSource entity);

        /// <summary>
        /// Confere se os valores da entidade são válidos.
        /// </summary>
        /// <param name="entity">Entidade para conferência.</param>
        /// <returns>Retorna true se a entidade for válida. Caso contrário retorna false.</returns>
        bool IsValid(TSource entity);

        /// <summary>
        /// Adiciona a entidade indicada.
        /// </summary>
        /// <param name="entity">Entidade a ser incluída.</param>
        /// <returns>Retorna a entidade após a inclusão. Apresentando algum erro retorna null.</returns>
        TSource AddWithDependents(TSource entity);

        /// <summary>
        /// Exclui a entidade indicada e os registros dependetes.
        /// </summary>
        /// <param name="entity">Entidade a ser deletada.</param>
        /// <returns>Retorna true se a entidade for excluido. Caso contrário retorna false.</returns>
        bool DeleteWithDependents(TSource entity);

        // Função será implementada em versões futuras
        ///// <summary>
        ///// Atualiza a entidade indicada.
        ///// </summary>
        ///// <param name="entity">Entidade a ser atualizada.</param>
        ///// <returns>Retorna true se a entidade for atualizado. Caso contrário retorna false.</returns>
        //bool UpdateWithDependents(TSource entity);
    }

}

