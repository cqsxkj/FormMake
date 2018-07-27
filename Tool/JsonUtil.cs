using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace WindowMake.Tool
{
    public static class JsonUtil
    {
        public static string Serialize(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            var stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            string dataString = Encoding.UTF8.GetString(dataBytes);
            stream.Dispose();
            return dataString;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
