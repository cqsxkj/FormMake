/* 功能：Datatable与实体类转换
 * 
 * 
 * 
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace WindowMake.DB
{
    public static class DataTableExtionsion
    {
        /// <summary>
        /// 把制定的DataTable转换为T类型的列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="datatable">数据表</param>
        /// <returns>实体列表</returns>
        public static IList<T> ToEntity<T>(this DataTable datatable) where T : new()
        {
            IList<T> results = new List<T>();

            PropertyInfo[] properyInfos = typeof(T).GetProperties();

            //CBNX
            if (null == datatable)
            {
                return results;
            }

            foreach (DataRow row in datatable.Rows)
            {
                T r = row.Row2Entity<T>();
                if (r != null)
                {
                    results.Add(r);
                }
            }

            return results;
        }

        public static T Row2Entity<T>(this DataRow row) where T : new()
        {
            if (row == null)
            {
                return default(T);
            }
            T r = new T();
            try
            {
                PropertyInfo[] properyInfos = typeof(T).GetProperties();
                for (int i = 0; i < properyInfos.Length; ++i)
                {
                    PropertyInfo propery = properyInfos[i];
                    if (!row.Table.Columns.Contains(propery.Name))
                    {
                        continue;
                    }

                    object value = row[propery.Name];
                    if (value == DBNull.Value)
                    {
                        continue;
                    }

                    propery.SetValue(r, GetValue(value, propery.PropertyType), null);
                }
            }
            catch (Exception)
            {
            }
            return r;
        }
        private static object GetValue(object obj, Type targetType)
        {

            if (targetType.IsAssignableFrom(typeof(int)))
            {
                return int.Parse(obj.ToString());
            }
            else if (targetType.IsAssignableFrom(typeof(double)))
            {
                return double.Parse(obj.ToString());
            }
            else if (targetType.IsAssignableFrom(typeof(long)))
            {
                return long.Parse(obj.ToString());
            }
            else if (targetType.IsAssignableFrom(typeof(decimal)))
            {
                return decimal.Parse(obj.ToString());
            }
            else if (targetType.IsAssignableFrom(typeof(float)))
            {
                return float.Parse(obj.ToString());
            }
            else if (targetType.IsAssignableFrom(typeof(string)))
            {
                return obj.ToString();
            }
            else if (targetType.IsAssignableFrom(typeof(decimal?)))
            {
                return obj as Nullable<decimal>;
            }
            else if (targetType.IsAssignableFrom(typeof(int?)))
            {
                return obj as Nullable<int>;
            }
            else if (targetType.IsAssignableFrom(typeof(double?)))
            {
                return obj as Nullable<double>;
            }
            else if (targetType.IsAssignableFrom(typeof(long?)))
            {
                return obj as Nullable<long>;
            }
            else if (targetType.IsAssignableFrom(typeof(float?)))
            {
                return obj as Nullable<float>;
            }
            return obj;
        }
    }
}
