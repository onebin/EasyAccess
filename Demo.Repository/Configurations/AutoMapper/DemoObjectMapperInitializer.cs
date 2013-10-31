using AutoMapper;

namespace Demo.Repository.Configurations.AutoMapper
{
    public class DemoObjectMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<DemoProfile>());
        } 
    }
}