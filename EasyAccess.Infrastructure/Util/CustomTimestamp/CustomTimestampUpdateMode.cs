namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    public enum CustomTimestampUpdateMode
    {
        /// <summary>
        /// 当实体数据的时间戳的原始值 等于 数据库中对应数据的时间戳时,执行更新操作
        /// </summary>
        Equal,

        /// <summary>
        ///  当实体数据的时间戳的当前值 大于 数据库中对应数据的时间戳时,执行更新操作
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 当实体数据的时间戳的当前值 大于 或其原始值 等于 数据库中对应数据的时间戳时,执行更新操作
        /// </summary>
        GreaterThanOrEqual
    }
}