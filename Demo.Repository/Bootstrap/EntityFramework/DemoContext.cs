using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Demo.Model.EDMs;

namespace Demo.Repository.Bootstrap.EntityFramework
{
    public partial class DemoContext
    {
        partial void CreateModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<SectionConfig>()
                        .HasMany(x => x.SubSections)
                        .WithOptional(x => x.ParentSection)
                        .HasForeignKey(x => x.ParentId);

            modelBuilder.Entity<InputConfig>()
                        .HasRequired(x => x.Section)
                        .WithOptional(x => x.Input)
                        .WillCascadeOnDelete(true);
        }
    }
}