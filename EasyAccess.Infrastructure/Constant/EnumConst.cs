using EasyAccess.Infrastructure.Attr;

namespace EasyAccess.Infrastructure.Constant
{
    /// <summary>
    /// 操作结果状态
    /// </summary>
    public enum StatusCode
    {

        /// <summary>
        /// 成功
        /// </summary>
        OK = 200,

        /// <summary>
        /// 未授权
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// 禁止访问
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// 未找到
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 417,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 500,

        /// <summary>
        /// 未实现
        /// </summary>
        NotImplemented = 501,
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 添加
        /// </summary>
        [EnumDescription("添加")]
        Insert = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [EnumDescription("修改")]
        Update = 2,

        /// <summary>
        /// 保存
        /// </summary>
        [EnumDescription("添加/修改")]
        Upsert = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [EnumDescription("删除")]
        Delete = 0
    }
}
