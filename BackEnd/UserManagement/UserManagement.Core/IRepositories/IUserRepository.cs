using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.IRepositories
{

    public interface IUserRepository : IGenericRepository<UserEntity, string>
    {
    }

}
