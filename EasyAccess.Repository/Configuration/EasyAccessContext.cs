using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configuration
{
    public class EasyAccessContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; } 

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Account>()
                .HasMany(x => x.Roles)
                .WithMany(x=>x.Accounts)
                .Map(x => x.MapLeftKey("RoleId").MapRightKey("AccountId").ToTable("AccountRole"));
            modelBuilder.Entity<Role>()
                .HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .Map(x => x.MapLeftKey("PermissionId").MapRightKey("RoleId").ToTable("RolePermission"));
            modelBuilder.Entity<Menu>()
                .HasMany(x => x.Permissions)
                .WithRequired(x => x.Menu);
            modelBuilder.Entity<Menu>()
                .HasOptional(x => x.ParentMenu)
                .WithMany(x => x.SubMenus)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
