using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.EntityFramework
{
    public class DemoContext : DbContext
    {
        public DbSet<FormConfig> Subjects { get; set; }

        public DbSet<ArticleConfig> ArticleConfigs { get; set; }

        public DbSet<SectionConfig> SectionConfigs { get; set; }

        public DbSet<InputConfig> InputConfigs { get; set; }

        public DbSet<DataCollection> DataCollections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<FormConfig>()
                        .HasMany(x => x.DataCollections)
                        .WithRequired(x => x.Subject)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<ArticleConfig>()
                        .HasMany(x => x.Sections)
                        .WithRequired(x => x.Article)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<SectionConfig>()
                        .HasOptional(x => x.ParentSection)
                        .WithMany(x => x.SubSections)
                        .HasForeignKey(x => x.ParentId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<SectionConfig>()
                        .HasMany(x => x.DataCollections)
                        .WithRequired(x => x.Section)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<InputConfig>()
                        .HasRequired(x => x.Section)
                        .WithOptional(x => x.Input)
                        .WillCascadeOnDelete(true);
        }
    }
}