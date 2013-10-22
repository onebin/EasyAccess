using EasyAccess.Repository.Configurations;
using EasyAccess.Repository.Configurations.AutoMapperConfig;
using EasyAccess.Repository.Configurations.EntityFrameworkConfig;

namespace EasyAccess.Service.Configurations
{
    public static class EasyAccessServiceInitializer
    {

        public static void DatabaseInitialize()
        {
            EasyAccessDatabaseInitializer.Initialize();
            EasyAccessObjectMapperInitializer.Initialize();
        }
    }
}
