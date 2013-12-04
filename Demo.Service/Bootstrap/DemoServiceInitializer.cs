using Demo.Repository.Bootstrap.EntityFramework;

namespace Demo.Service.Bootstrap
{
    public static class DemoServiceInitializer
    {
        public static void DatabaseInitialize()
        {
            DemoDatabaseInitializer.Initialize();
        }
    }
}
