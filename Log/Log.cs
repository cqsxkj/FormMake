using System;
using System.IO;

namespace WindowMake
{
    public class Log
    {
        private static string logStr = string.Empty;
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            if (msg.Equals(logStr))
            {
                return;
            }
            ////该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "   " + msg);
                    logStr = msg;
                }
            }
        }
        /// <summary>
         /// 记录日志
         /// </summary>
         /// <param name="msg"></param>
        public static void WriteLog(object obj)
        {
            ////该日志文件会存在windows服务程序目录下
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log.txt";
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "   " + obj);
                }
            }
        }
    }
}
