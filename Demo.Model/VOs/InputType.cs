namespace Demo.Model.VOs
{
    public enum InputType
    {
        /// <summary>
        /// 文本
        /// </summary>
        ValidateBox = 11,

        /// <summary>
        /// 数字
        /// </summary>
        NumberBox = 12,


        /// <summary>
        /// 下拉列表(单选)
        /// </summary>
        SingleComboBox = 21,

        /// <summary>
        /// 下拉列表(多选)
        /// </summary>
        MultiComboBox = 22,



        /// <summary>
        /// 日期
        /// </summary>
        DateBox = 31,

        /// <summary>
        /// 时间
        /// </summary>
        TimeSpinner = 32,

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTimeBox = 33

    }
}
