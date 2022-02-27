using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : IdentityUser<TKey>
                where TKey : IEquatable<TKey>
    {
        protected UserManagementDbContext context;
        protected DbSet<TEntity> dbSet;


        public GenericRepository(UserManagementDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual bool Add(TEntity entity)
        {
            dbSet.Add(entity);
            return true;
        }


        public virtual async Task<TEntity> GetByEmail(string email)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Email == email);

            if (entity == null)
                throw new Exception("Can't find record");

            return entity;
        }
    }
}
