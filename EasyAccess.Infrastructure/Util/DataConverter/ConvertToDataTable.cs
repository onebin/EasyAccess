using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EasyAccess.Infrastructure.Util.DataConverter
{
    public static class ConvertToDataTable
    {

        public static DataTable ToDataTable<T>(this DataConverter<T> dataConverter, ICollection<T> listData) where T : class
        {
            return listData == null ? null : Conver(dataConverter, listData);
        }

        private static DataTable Conver<T>(DataConverter<T> dataConverter, ICollection<T> listData) where T : class
        {
            var dataTable = new DataTable();
            var type = typeof(T);
            dataTable.TableName = type.Name;
            if (listData.Count > 0)
            {
                string fieldName;
                dataTable.Columns.AddRange(dataConverter.PropertyToShow.Select(x => new DataColumn(dataConverter.FieldFormatter.TryGetValue(x.Name, out fieldName) ? fieldName : x.Name, typeof(string))).ToArray());

                foreach (var data in listData)
                {
                    var dataRow = dataTable.NewRow();
                    foreach (var property in dataConverter.PropertyToShow)
                    {
                        string key, val;
                        dataConverter.GetKeyValFromDataProperty(data, property, out key, out val);
                        dataRow.SetField(key, val);
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
    }
}
