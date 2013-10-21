namespace EasyAccess.Model.DTOs
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// 菜单项Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父菜单项Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单项链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 菜单类别
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; set; }
    }
}
