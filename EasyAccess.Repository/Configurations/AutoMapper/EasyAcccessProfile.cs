using AutoMapper;

namespace EasyAccess.Repository.Configurations.AutoMapper
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