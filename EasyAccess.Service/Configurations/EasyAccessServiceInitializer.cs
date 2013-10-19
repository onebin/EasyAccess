using EasyAccess.Repository.Configurations;

namespace EasyAccess.Service.Configurations
{
    public class EasyAccessServiceInitializer
    {

        public static void DatabaseInitialize()
        {
            EasyAccessDatabaseInitializer.Initialize();
        }
    }
}
