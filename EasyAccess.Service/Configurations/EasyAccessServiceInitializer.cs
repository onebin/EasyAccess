using EasyAccess.Repository.Configurations;
using EasyAccess.Repository.Configurations.EntityFramework;

namespace EasyAccess.Service.Configurations
{
    public static class EasyAccessServiceInitializer
    {

        public static void DatabaseInitialize()
        {
            EasyAccessDatabaseInitializer.Initialize();
        }
    }
}
