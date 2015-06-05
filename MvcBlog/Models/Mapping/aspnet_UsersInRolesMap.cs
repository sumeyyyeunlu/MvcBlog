using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MvcBlog.Models.Mapping
{
    public class aspnet_UsersInRolesMap : EntityTypeConfiguration<aspnet_UsersInRoles>
    {
        public aspnet_UsersInRolesMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserId, t.RoleId });

            // Properties
            // Table & Column Mappings
            this.ToTable("aspnet_UsersInRoles");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            // Relationships
            this.HasRequired(t => t.aspnet_Roles)
                .WithMany(t => t.aspnet_UsersInRoles)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
