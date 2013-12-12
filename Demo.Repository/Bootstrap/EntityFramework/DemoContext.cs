﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Demo.Model.EDMs;

namespace Demo.Repository.Bootstrap.EntityFramework
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