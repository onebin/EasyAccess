using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Demo.Model.EDMs;

namespace Demo.Repository.Bootstrap.EntityFramework
{
	public partial class DemoContext : DbContext
    { 
		public DbSet<FormConfig> FormConfigs { get; set; }
	 
		public DbSet<ArticleConfig> ArticleConfigs { get; set; }
	 
		public DbSet<DataCollection> DataCollections { get; set; }
	 
		public DbSet<InputConfig> InputConfigs { get; set; }
	 
		public DbSet<SectionConfig> SectionConfigs { get; set; }
	
		partial void CreateModel(DbModelBuilder modelBuilder);

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            CreateModel(modelBuilder);
		}
    }
}		
