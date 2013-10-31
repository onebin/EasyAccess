using AutoMapper;
using Demo.Model.DTOs;
using Demo.Model.EDMs;

namespace Demo.Repository.Configurations.AutoMapper
{
    public class DemoProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SectionConfig, SectionConfigDto>();
            Mapper.CreateMap<SectionConfigDto, SectionConfig>();

            Mapper.CreateMap<ArticleConfig, ArticleConfigDto>();
            Mapper.CreateMap<ArticleConfigDto, ArticleConfig>();

            Mapper.CreateMap<InputConfig, InputConfigDto>();
            Mapper.CreateMap<InputConfigDto, InputConfig>();
        }
    }
}