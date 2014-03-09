using System;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
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
}