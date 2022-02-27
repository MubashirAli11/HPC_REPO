using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, string>, IUserRepository
    {
        public UserRepository(UserManagementDbContext context) : base(context)
        {
        }
    }
}
