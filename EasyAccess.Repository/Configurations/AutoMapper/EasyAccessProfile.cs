using AutoMapper;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configurations.AutoMapper
{
    public class EasyAccessProfile : Profile
    {
        protected override void Configure()
        {
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            Mapper.CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.NickName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contact.Phone));

        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        } 
    }
}