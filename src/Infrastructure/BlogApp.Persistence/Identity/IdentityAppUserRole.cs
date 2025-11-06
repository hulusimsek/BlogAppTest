using Microsoft.AspNetCore.Identity;
using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Identity
{
    public class IdentityAppUserRole : IdentityUserRole<Guid>
    {
        public DateTime AssignedDate { get; set; }

        public IdentityAppUser User { get; set; } = null!;
        public IdentityAppRole Role { get; set; } = null!;
        public IdentityAppUserRole()
        {
            AssignedDate = DateTime.UtcNow;
        }
    }

}
