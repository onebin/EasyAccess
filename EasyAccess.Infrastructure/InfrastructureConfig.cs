using CK1.EasyFramework.Infrastructure.Util.CustomTimestamp;

namespace EasyAccess.Infrastructure
{
    public static class InfrastructureConfig
    {
        public static CustomTimestampUpdateOption CustomTimestampUpdateOption { get; set; }

        static InfrastructureConfig()
        {
            CustomTimestampUpdateOption = CustomTimestampUpdateOption.Disable;
        }
    }
}