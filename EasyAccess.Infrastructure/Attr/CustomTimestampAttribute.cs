using System;

namespace EasyAccess.Infrastructure.Attr
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomTimestampAttribute : Attribute
    {
        public CustomTimestampUpdateMode UpdateMode { get; private set; }

        public CustomTimestampAttribute(CustomTimestampUpdateMode updateMode)
        {
            UpdateMode = updateMode;
        }
    }

    public enum CustomTimestampUpdateMode
    {
        Equal,

        GreaterThanOrEqual,

        GreaterThan
    }
}