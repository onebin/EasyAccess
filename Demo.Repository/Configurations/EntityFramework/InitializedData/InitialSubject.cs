using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Model.EDMs;
using Demo.Repository.Configurations.EntityFramework.InitializedData.Seed;

namespace Demo.Repository.Configurations.EntityFramework.InitializedData
{
    internal static class InitialSubject
    {
        public static void Initialize(DbContext context)
        {
            var articleConfigSet = context.Set<ArticleConfig>();
            var inputConfigSet = context.Set<InputConfig>();
            var sectionConfigSet = context.Set<SectionConfig>();
            var subjectSet = context.Set<Subject>();

            subjectSet.AddOrUpdate(x => x.Id, SubjectSeed.Values);
            articleConfigSet.AddOrUpdate(x => x.Id, ArticleConfigSeed.Values);
            inputConfigSet.AddOrUpdate(x => x.Id, InputConfigSeed.Values);
            sectionConfigSet.AddOrUpdate(x => x.Id, SectionConfigSeed.Values);
        }
    }
}
