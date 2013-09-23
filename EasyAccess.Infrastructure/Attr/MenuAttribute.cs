using System;
using System.Text.RegularExpressions;

namespace EasyAccess.Infrastructure.Attr
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute() { }

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
            }
        }

        public MenuAttribute(string id, string name, string url)
            :this(id,name)
        {
            this.Url = url;
        }

        public MenuAttribute(string id, string name, string url, string parentId)
            : this(id, name, url)
        {
            this.ParentId = parentId;
        }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string System { get; set; }

        public string Url { get; set; }

        public int Depth { get; set; }

        public int Index { get; set; }
    }
}
