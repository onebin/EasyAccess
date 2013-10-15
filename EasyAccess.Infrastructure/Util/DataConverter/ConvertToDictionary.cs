using System.Collections.Generic;
using System.Linq;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public static class ConvertToDictionary
    {
        public static Dictionary<string, object> ToDictionary<T>(this DataConverter<T> dataConverter, T data) where T : class
        {
            return data == null ? null : Conver(dataConverter, new List<T>() { data }).FirstOrDefault();
        }

        public static List<Dictionary<string, object>> ToDictionary<T>(this DataConverter<T> dataConverter, ICollection<T> listData) where T : class
        {
            return listData == null ? null : Conver(dataConverter, listData);
        }

        private static List<Dictionary<string, object>> Conver<T>(DataConverter<T> dataConverter, ICollection<T> listData) where T : class
        {
            var lst = new List<Dictionary<string, object>>();
            if (listData.Count > 0)
            {
                foreach (var data in listData)
                {
                    var item = new Dictionary<string, object>();
                    foreach (var property in dataConverter.PropertyToShow)
                    {
                        string key, val;
                        dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                        item.Add(key, val);
                    }
                    lst.Add(item);
                }
            }
            return lst;
        }
    }
}
