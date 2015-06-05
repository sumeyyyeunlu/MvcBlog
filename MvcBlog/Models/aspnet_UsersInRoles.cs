using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class aspnet_UsersInRoles
    {
        public System.Guid UserId { get; set; }
        public System.Guid RoleId { get; set; }
        public virtual aspnet_Roles aspnet_Roles { get; set; }
    }
}
