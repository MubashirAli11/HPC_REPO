using Ship.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Core.IRepositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetById(TKey id);
        bool Add(TEntity entity);
        Task<bool> Delete(TKey id);
        Task<bool> Update(TKey id, TEntity entity);
        Task<(IEnumerable<TEntity>, int)> GetList(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);
    }
}
