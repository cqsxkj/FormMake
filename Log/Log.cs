using System;
using System.Diagnostics;
using System.IO;

namespace WindowMake
{
    public class Log
    {
        public long m_FileMaxLength = 1024 * 1024 * 5;//日志文件大小限制
        public int m_FileMaxNum = 2;//日志文件个数
        object logLock = new object();               // 日志锁
        #region
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logstring">日志信息</param>
        /// <param name="leve">等级，1-debug,2-insertDB,3-sys</param>
        public void WriteLog(object logstring, int leve = 0)
        {
            logstring = DateTime.Now.ToString() + " " + logstring;
            WriteTxtNoTime(logstring, leve);
        }
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logstring">日志信息</param>
        /// <param name="leve">等级，1-debug,2-insertDB,3-sys</param>
        public void WriteLog(string logstring, int leve = 0)
        {
            logstring = DateTime.Now.ToString() + " " + logstring;
            WriteTxtNoTime(logstring, leve);
        }
        #endregion
        public void WriteTxtNoTime(object logstring, int leve = 0)
        {
            string FileName, logPaths, errorString;
            switch (leve)
            {
                case 1:
                    FileName = "deubg.log";
                    break;
                case 2:
                    FileName = "insertDB.log";
                    break;
                default:
                    FileName = "sys.log";
                    break;
            }
            logPaths = AppDomain.CurrentDomain.BaseDirectory + FileName;
            errorString = logstring + "\n";
            try
            {
                lock (logLock)
                {
                    StreamWriter sw = null;
                    sw = File.AppendText(logPaths);
                    sw.WriteLine(logstring);
                    sw.Flush();
                    sw.Close();
                    NeedNewFile(FileName);
                }
            }
            catch (System.Exception)
            { }
        }
        public void NeedNewFile(string FileName)
        {
            FileInfo finfo = new FileInfo(FileName);
            if (finfo.Length < m_FileMaxLength)
                return;
            string NameHead, FileFormat, curName, ChangName;
            int p = FileName.IndexOf(".");
            NameHead = FileName.Substring(0, p);
            FileFormat = FileName.Substring(p, FileName.Length - p);
            for (int i = m_FileMaxNum; i > 0; i--)
            {
                curName = NameHead + i + FileFormat;
                try
                {
                    File.Delete(curName);
                }
                catch (Exception) { Trace.WriteLine("删除文件%s失败", curName); }
                if (1 == i)
                    ChangName = NameHead + FileFormat;
                else
                    ChangName = NameHead + (i - 1) + FileFormat;
                try
                {
                    File.Move(ChangName, curName);
                }
                catch (Exception ex) { Trace.WriteLine("重命名文件%s失败" + ex.ToString(), curName); }
            }
        }
    }
}
