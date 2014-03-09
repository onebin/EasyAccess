using System;
using System.Text.RegularExpressions;

namespace EasyAccess.Authorization.Attr
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAttribute : Attribute
    {
        public PermissionAttribute() { }

        public PermissionAttribute(string id)
        {
            this.Id = id;
            this.Dependent = true;
        }

        public PermissionAttribute(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            Match matchResults = Regex.Match(Id, @"^\D*\d*");
            this.MenuId = matchResults.Success ? matchResults.Value : null;
        }

        public PermissionAttribute(string id, string name, string actionUrl)
            : this(id, name)
        {
            this.ActionUrl = actionUrl;
        }

        public string Id { get; set; }

        public string MenuId { get; set; }

        public string Name { get; set; }
        
        public string ActionUrl { get; set; }

        public bool UnverifyByFilter { get; set; }

        public bool Dependent { get; private set; }
    }
}
