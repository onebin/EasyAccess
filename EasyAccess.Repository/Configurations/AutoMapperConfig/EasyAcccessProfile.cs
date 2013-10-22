using AutoMapper;
using EasyAccess.Model.DTOs;
using EasyAccess.Model.EDMs;

namespace EasyAccess.Repository.Configurations.AutoMapperConfig
{
    public class EasyAcccessProfile : Profile
    {
        protected override void Configure()
        {
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        } 
    }
}