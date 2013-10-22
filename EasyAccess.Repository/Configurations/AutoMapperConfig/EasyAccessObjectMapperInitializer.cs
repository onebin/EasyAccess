using AutoMapper;

namespace EasyAccess.Repository.Configurations.AutoMapperConfig
{
    public static class EasyAccessObjectMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<EasyAcccessProfile>());
        }
    }
}
