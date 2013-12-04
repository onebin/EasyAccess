namespace EasyAccess.Infrastructure.Entity
{
    public interface ISoftDelete
    {
        /// <summary>
        /// 逻辑上的删除，非物理删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
