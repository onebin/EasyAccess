using EasyAccess.Repository.Bootstrap.EntityFramework;

namespace EasyAccess.Service.Bootstrap
{
    public static class EasyAccessServiceInitializer
    {

        public static void DatabaseInitialize()
        {
            EasyAccessDatabaseInitializer.Initialize();
        }
    }
}
