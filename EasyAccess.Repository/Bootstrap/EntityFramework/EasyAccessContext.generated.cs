using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Bootstrap.EntityFramework
{
    public partial class EasyAccessContext : DbContext
    { 
		public DbSet<Account> Accounts { get; set; }
	 
		public DbSet<Menu> Menus { get; set; }
	 
		public DbSet<Permission> Permissions { get; set; }
	 
		public DbSet<Register> Registers { get; set; }
	 
		public DbSet<Role> Roles { get; set; }
	
		partial void CreateModel(DbModelBuilder modelBuilder);

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            CreateModel(modelBuilder);
		}
    }
}			
