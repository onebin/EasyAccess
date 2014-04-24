using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    internal interface ICustomTimestampCache
    {
        bool HasTimestampProperty { get; }
        
        string TableName { get;  }
        
        string TimestampColumnName { get; }
        
        string TimestampPropertyName { get; }
        
        CustomTimestampUpdateMode UpdateMode { get; }
        
        PropertyInfo[] BasicProperies { get; }

        PropertyInfo[] Properies { get; }

        string GetColumnName(PropertyInfo property);

        object GetColumnValue(PropertyInfo property, DbEntityEntry entry);
    }
}