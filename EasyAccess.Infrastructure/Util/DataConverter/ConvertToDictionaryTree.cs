using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyAccess.Infrastructure.Extensions;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public static class ConvertToDictionaryTree
    {
        private static string _idFieldName, _pidFieldName, _childrenFieldName;
        private static PropertyInfo _pidProperty;

        public static List<Dictionary<string, object>> ToDictionaryTree<T, TKey>(
            this DataConverter<T> dataConverter,
            IList<T> listData,
            string childrenFieldName = "children",
            string idFieldName = "Id",
            string pidFieldName = "ParentId",
            Dictionary<string, object> rootNode = null,
            object rootIdValue = null
            )
            where T : class
            where TKey : struct
        {
            _idFieldName = idFieldName;
            _pidFieldName = pidFieldName;
            _childrenFieldName = childrenFieldName;
            _pidProperty = dataConverter.PropertyToShow.FirstOrDefault(x => x.PropertyType.GetNonNullableType() == typeof(TKey) && x.Name == pidFieldName);
            return listData == null ? null : Conver<T, TKey>(dataConverter, listData, rootNode, rootIdValue);
        }

        private static List<Dictionary<string, object>> Conver<T, TKey>(
            DataConverter<T> dataConverter,
            ICollection<T> listData,
            Dictionary<string, object> rootNode,
            object rootIdValue
            )
            where T : class
            where TKey : struct
        {
            var root = new List<Dictionary<string, object>>();
            if (listData.Count > 0)
            {
                if (rootNode != null)
                {
                    rootNode.Add(_childrenFieldName, GetChildren(dataConverter, listData, rootIdValue));
                    root.Add(rootNode);
                }
                else
                {
                    root.AddRange(GetChildren(dataConverter, listData, rootIdValue));
                }
            }
            else
            {
                root.Add(rootNode);
            }
            return root;
        }

        private static List<Dictionary<string, object>> GetChildren<T>(
            DataConverter<T> dataConverter,
            ICollection<T> listData,
            object pidValue,
            int level = 0
            ) where T : class
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
                    if (property.Name.Equals(_idFieldName))
                    {
                        pidVal = val;
                    }
                }
                child.Add(_childrenFieldName, GetChildren(dataConverter, noParentData.ToList(), pidVal, ++level));
                children.Add(child);
            }
            return children;
        }
    }
}
