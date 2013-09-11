using System;
using System.Collections.Generic;
using System.Data.Entity;
using EasyAccess.Models;

namespace EasyAccess.Infrastructure.Repositories
{
    public class EasyAccessInitializer : DropCreateDatabaseIfModelChanges<EasyAccessContext>
    {
        protected override void Seed(EasyAccessContext context)
        {

        }
    }
}
