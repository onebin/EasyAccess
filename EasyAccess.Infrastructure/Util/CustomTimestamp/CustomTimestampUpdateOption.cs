namespace CK1.EasyFramework.Infrastructure.Util.CustomTimestamp
{
    public enum CustomTimestampUpdateOption
    {
        /// <summary>
        /// 关闭自定义时间戳更新
        /// </summary>
        Disable,

        /// <summary>
        /// 仅更新被标记自定时间戳的实体
        /// </summary>
        CustomTimestampOnly,

        /// <summary>
        /// 自动识别
        /// </summary>
        Auto
    }
}