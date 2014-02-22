using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public static class ConvertToDictionaryTree
    {
        private static PropertyInfo _pidProperty;
        private static ConvertToDictionaryTreeOptions _options;

        public static List<Dictionary<string, object>> ToDictionaryTree<T, TKey>(
            this DataConverter<T> dataConverter,
            IList<T> listData,
            ConvertToDictionaryTreeOptions options = null) where T : class where TKey : struct
        {
            _options = options ?? new ConvertToDictionaryTreeOptions();
            _pidProperty = dataConverter.PropertyToShow.FirstOrDefault(x => x.PropertyType.GetNonNullableType() == typeof(TKey) && x.Name == _options.PidFieldName);
            return Conver(dataConverter, listData);
        }

        private static List<Dictionary<string, object>> Conver<T>(
            DataConverter<T> dataConverter,
            ICollection<T> listData) where T : class
        {
            var root = new List<Dictionary<string, object>>();
            if (listData != null && listData.Count > 0)
            {
                if (_options.RootNode != null)
                {
                    _options.RootNode.Add(_options.ChildrenFieldName, GetChildren(dataConverter, listData, _options.RootIdValue));
                    root.Add(_options.RootNode);
                }
                else
                {
                    root.AddRange(GetChildren(dataConverter, listData, _options.RootIdValue));
                }
            }
            else if (_options.RootNode != null)
            {
                root.Add(_options.RootNode);
            }
            return root;
        }

        private static List<Dictionary<string, object>> GetChildren<T>(
            DataConverter<T> dataConverter,
            ICollection<T> listData,
            object pidValue) where T : class
        {
            var children = new List<Dictionary<string, object>>();


            var subData = listData.Where(x => pidValue == null ? _pidProperty.GetValue(x, null) == null : _pidProperty.GetValue(x, null) != null && _pidProperty.GetValue(x, null).ToString() == pidValue.ToString());
            var noParentData = listData.Where(x => pidValue == null ? _pidProperty.GetValue(x, null) != null : _pidProperty.GetValue(x, null) != null && _pidProperty.GetValue(x, null).ToString() != pidValue.ToString());
            foreach (var data in subData)
            {
                var child = new Dictionary<string, object>();
                object pidVal = null;
                foreach (var property in dataConverter.PropertyToShow)
                {
                    string key, val;
                    dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                    child.Add(key, val);
                    if (property.Name.Equals(_options.IdFieldName))
                    {
                        pidVal = val;
                    }
                }
                child.Add(_options.ChildrenFieldName, GetChildren(dataConverter, noParentData.ToList(), pidVal));
                children.Add(child);
            }
            return children;
        }
    }

    public class ConvertToDictionaryTreeOptions
    {
        public ConvertToDictionaryTreeOptions()
        {
            ChildrenFieldName = "children";
            IdFieldName = "Id";
            PidFieldName = "ParentId";
            RootNode = null;
            RootIdValue = null;
        }

        public string ChildrenFieldName { get; set; }
        public string IdFieldName { get; set; }
        public string PidFieldName { get; set; }
        public Dictionary<string, object> RootNode { get; set; }
        public object RootIdValue { get; set; }}
}
