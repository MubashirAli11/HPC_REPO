
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Entities
{
    public class UserEntity : IdentityUser<string>
    {
        public bool IsDeleted { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; private set; }


        public void MarkDeleted()
        {
            this.IsDeleted = true;
            this.ModifiedOn = DateTime.UtcNow;
        }

        public void Modified()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }

        public UserEntity(string userName, string email, string phoneNumber)
        {
            this.UserName = userName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.CreatedOn = DateTime.UtcNow;
        }

        public void AddId()
        {
            this.Id = Guid.NewGuid().ToString();
        }


    }
}
