using System;
using System.Text.RegularExpressions;

namespace EasyAccess.Authorization.Attr
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="name">菜单名称</param>
        public MenuAttribute(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            var sysMatch = Regex.Match(Id, @"^\D*");
            var depth = new Regex(@"\d{2}");
            this.System = sysMatch.Success ? sysMatch.Value : null;
            this.Depth = depth.Matches(id).Count;
            if (this.Depth > 1)
            {
                this.ParentId = this.Id.Substring(0, this.Id.Length - 2);
                this.Index = int.Parse(this.Id.Substring(this.Id.Length - 2, 2));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="name">菜单名称</param>
        /// <param name="index">排序索引</param>
        public MenuAttribute(string id, string name, int index)
            :this(id, name)
        {
            this.Index = index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="name">菜单名称</param>
        /// <param name="url">菜单链接</param>
        public MenuAttribute(string id, string name, string url)
            :this(id,name)
        {
            this.Url = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="name">菜单名称</param>
        /// <param name="url">菜单链接</param>
        /// <param name="index">排序索引</param>
        public MenuAttribute(string id, string name, string url, int index)
            : this(id, name, url)
        {
            this.Index = index;
        }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string System { get; private set; }

        public string Url { get; set; }

        public int Depth { get; private set; }

        public int Index { get; set; }
    }
}
