using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Infrastructure.DbConfiguration;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementDbContext : IdentityDbContext<UserEntity, IdentityRole<string>, string, IdentityUserClaim<string>, IdentityUserRole<string>,
            IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());         
            modelBuilder.ApplyConfiguration(new UserLoginLogEntityConfiguration());
        }

    }
}
