using Demo.Repository.Configurations.EntityFramework;

namespace Demo.Service.Configurations
{
    public static class DemoServiceInitializer
    {
        public static void DatabaseInitialize()
        {
            DemoDatabaseInitializer.Initialize();
        }
    }
}
