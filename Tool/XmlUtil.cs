using System;
using System.IO;
using System.Xml.Serialization;

namespace WindowMake.Tool
{
    public class XmlUtil
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, string defaultNamespace) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    if (!string.IsNullOrEmpty(defaultNamespace))
                    {
                        xmldes = new XmlSerializer(typeof(T), defaultNamespace);
                    }
                    return xmldes.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("xml反序列化" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 反序列化XML
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xmlFilePath">XML地址</param>
        /// <returns>反序列化后的数据</returns>
        public static T XmlDeserializerEx<T>(string xmlFilePath)
        {
            if (!System.IO.File.Exists(xmlFilePath))
            {
                throw new ArgumentNullException(xmlFilePath + "文件不存在");
            }
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(xmlFilePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    return (T)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("xml序列化" + e.Message);
                throw e;
            }
        }
        #endregion
        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();

            return str;
        }
        #endregion
    }
}
