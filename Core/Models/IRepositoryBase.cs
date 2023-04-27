using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Models
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Obter(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task DeletarAsync(TEntity entity);
        Task Atualizar(TEntity entity);
    }
}