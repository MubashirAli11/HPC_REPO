using Microsoft.EntityFrameworkCore;
using Ship.Core.Entities;
using Ship.Core.IRepositories;
using Ship.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ship.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected ShipDataContext context;
        protected DbSet<TEntity> dbSet;


        public GenericRepository(ShipDataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual bool Add(TEntity entity)
        {
            dbSet.Add(entity);
            return true;
        }

        public virtual async Task<bool> Delete(TKey id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
                throw new Exception("Can't find record");

            entity.MarkDeleted();

            dbSet.Update(entity);

            return true;
        }

        public virtual async Task<(IEnumerable<TEntity>, int)> GetList(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            var query = dbSet.Where(predicate);

            var count = await query.CountAsync();

            var response = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (response, count);
        }

        public virtual async Task<TEntity> GetById(TKey id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
                throw new Exception("Can't find record");

            return entity;
        }

        public virtual Task<bool> Update(TKey id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
