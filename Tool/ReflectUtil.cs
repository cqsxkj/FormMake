using System;
using System.Reflection;

namespace WindowMake.Tool
{
    public static class ReflectUtil
    {
        public static PropertyInfo[] GetProperties(Type type)
        {
            PropertyInfo[] props = null;
            try
            {
                object obj = Activator.CreateInstance(type);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            catch (Exception ex)
            { }
            return props;
        }
    }
}
