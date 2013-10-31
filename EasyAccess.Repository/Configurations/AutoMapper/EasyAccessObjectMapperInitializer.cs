using AutoMapper;

namespace EasyAccess.Repository.Configurations.AutoMapper
{
    public static class EasyAccessObjectMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<EasyAccessProfile>());
        }
    }
}
