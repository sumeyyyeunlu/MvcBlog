using System;
using System.Collections.Generic;

namespace MvcBlog.Models
{
    public partial class aspnet_Roles
    {
        public aspnet_Roles()
        {
            this.aspnet_UsersInRoles = new List<aspnet_UsersInRoles>();
        }

        public System.Guid ApplicationId { get; set; }
        public System.Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
        public virtual aspnet_Applications aspnet_Applications { get; set; }
        public virtual ICollection<aspnet_UsersInRoles> aspnet_UsersInRoles { get; set; }
    }
}
