using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.IRepositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : IdentityUser<TKey>
         where TKey : IEquatable<TKey>
    {
        Task<TEntity> GetByEmail(string email);
        bool Add(TEntity entity);
    }
}
