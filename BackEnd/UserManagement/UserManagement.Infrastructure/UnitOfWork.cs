using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.IRepositories;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UserManagementDbContext _context;

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(UserManagementDbContext context)
        {
            _context = context;
            this.UserRepository = new UserRepository(context);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
