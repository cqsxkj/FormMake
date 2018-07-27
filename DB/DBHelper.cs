/* 功能：数据库访问类
 * 时间：2017-8-24 09:27:56
 * 作者：张伟
 * 
 * 描述：
 * 
 * */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace WindowMake.DB
{
    internal class DBHelper
    {
        private static readonly string connectionString = System.Configuration.ConfigurationSettings.AppSettings["DbConnectionString"].ToString();
        private static MySqlTransaction transaction;
        private static MySqlConnection connection = new MySqlConnection(connectionString);
        private static bool DBalive;
        /// <summary>
        /// 执行SQL语句，事物
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>是否成功</returns>
        public static int ExcuteTransactionSql(string SQLString)
        {
            int i = -1;
            try
            {
                OpenConnection(connectionString);
            }
            catch (Exception)
            {
                Log.WriteLog("打开数据库失败！");
                return -1;
            }
            transaction = connection.BeginTransaction();
            using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
            {
                try
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText = SQLString;
                    i = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    transaction.Dispose();
                    return i;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    Log.WriteLog("ExcuteTransactionSql:" + e);
                    CloseConnection();
                    return i;
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数,-1表示失败
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExcuteSql(string SQLString)
        {
            try
            {
                OpenConnection(connectionString);
            }
            catch (Exception)
            {
                Log.WriteLog("打开数据库失败！");
                return -1;
            }
            using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
            {
                try
                {
                    object obj = cmd.ExecuteScalar();

                    return Convert.ToInt32(obj);
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    Log.WriteLog("ExcuteSql:" + e);
                    CloseConnection();
                    return -1;
                }
            }
        }
        private static void OpenConnection(string connectionString)
        {
            if (!DBalive)
            {
                connection.Open();
                DBalive = true;
                Log.WriteLog("数据库连接打开");
            }
        }

        private static void CloseConnection()
        {
            try
            {
                connection.Close();
                DBalive = false;
                Log.WriteLog("数据库连接关闭。");
            }
            catch (Exception)
            {
                Log.WriteLog("关闭连接异常!");
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            DataSet ds = new DataSet();
            try
            {
                OpenConnection(connectionString);
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                command.Fill(ds);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Log.WriteLog("ExcuteSql:" + ex);
            }
            return ds;
        }

        public static IList<T> Query<T>(string strSql) where T : new()
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

        internal static int Exists(string sql)
        {
            try
            {
                if (string.IsNullOrEmpty(sql))
                {
                    return -1;
                }
                return ExcuteTransactionSql(sql);
            }
            catch (Exception e)
            {
                Log.WriteLog("ExcuteSql:" + e);
                throw e;
            }
        }
    }
}
