/* 功能：读取access的mdb文件
 * 时间：2018-7-24 09:43:40
 * 作者：张伟
 * 
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace WindowMake.DB
{
    public class MDBHelper
    {
        private string _fileName;
        private string _connectionString;
        private OleDbConnection _odcConnection;
        
        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="fileName">MDB文件（含完整路径）</param>
        public MDBHelper(string fileName)
        {
            this._fileName = fileName;
            this._connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";";
        }

        /// <summary>
        /// 建立连接（打开数据库文件）
        /// </summary>
        public void Open()
        {
            try
            {
                // 建立连接
                this._odcConnection = new OleDbConnection(this._connectionString);

                // 打开连接
                this._odcConnection.Open();
            }
            catch (Exception e)
            {
                Log.WriteLog("尝试打开 " + this._fileName + " 失败, 请确认文件是否存在！");
                throw e;
            }
        }
        public IList<T> Query<T>(string strSql) where T : new()
        {
            try
            {
                DataTable tb = Query(strSql).Tables[0];
                return tb.ToEntity<T>();
            }
            catch (Exception e)
            {
                Log.WriteLog("ExcuteSql:" + e);
                throw e;
            }
        }

        /// <summary>
        /// 断开连接（关闭据库文件）
        /// </summary>
        public void Close()
        {
            this._odcConnection.Close();
        }


        /// <summary>
        /// 根据sql命令返回一个DataSet
        /// </summary>
        /// <param name="sql">sql命令</param>
        /// <returns>以DataTable形式返回数据</returns>
        public DataSet Query(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, this._odcConnection);
                adapter.Fill(ds);
            }
            catch (Exception)
            {
                Log.WriteLog("sql语句： " + sql + " 执行失败！");
                throw new Exception("sql语句： " + sql + " 执行失败！");
            }
            return ds;
        }
    }
}
