using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StuSiteMVC.DAL
{
    public static class SqlHelper
    {
        public static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        #region ExecuteNonQuery 执行SQL语句或存储过程后，返回受影响的行数。

        /// <summary>
        /// 执行SQL语句或存储过程后，返回受影响的行数。
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, CommandType.Text, conn, commandText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                return val;
            }
        }

        /// <summary>
        /// 执行SqlServer存储过程，注意：不能执行有OUT参数的存储过程
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数值</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, spName, parameterValues);
                int val = cmd.ExecuteNonQuery();
                return val;
            }
        }
        #endregion

        #region ExcuteScalar 执行SQL语句或者存储过程后，如插入一条新记录的时候返回自增的ID。

        /// <summary>
        /// 执行SQL语句或者存储过程后，如插入一条新记录的时候返回自增的ID。
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="commandParameters">参数值</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, CommandType.Text, conn, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }

        /// <summary>
        /// 执行SqlServer存储过程
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数值</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, spName, parameterValues);
                object val = cmd.ExecuteScalar();
                return val;
            }
        }
        #endregion

        #region ExecuteReader 执行SQL语句或存储过程后，返回一个DateReader。

        /// <summary>
        /// 执行SQL语句或存储过程后，返回一个DateReader。
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, CommandType.Text, conn, commandText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SqlServer存储过程
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数值</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, spName, parameterValues);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteDataset 执行SQL语句或者存储过程后，返回一个Dataset。

        /// <summary>
        /// 执行SQL语句或者存储过程后，返回一个Dataset。
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">sql字符串</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameter)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, CommandType.Text, conn, commandText, commandParameter);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行SqlServer存储过程
        /// </summary>
        /// <param name="connectionString">sql连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数值</param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, spName, parameterValues);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        #endregion

        #region PrepareCommand 构建一个Command对象供内部方法进行调用，两个重载方法。

        /// <summary>
        /// 构建一个Command对象供内部方法进行调用，两个重载方法。
        /// </summary>
        /// <param name="cmd">SqlCommand对象，不允许为空对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="conn">SqlConnection对象，不允许为空对象</param>
        /// <param name="commandText">sql语句</param>
        /// <param name="cmdParms">参数数组，允许为空</param>
        private static void PrepareCommand(SqlCommand cmd, CommandType commandType, SqlConnection conn, string commandText, SqlParameter[] cmdParms)
        {
            conn.Close();
            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            //设置Sqlcommand对象
            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 设置一个等待执行存储过程的SQLCommand对象
        /// </summary>
        /// <param name="cmd">SqlCommand对象，不允许为空</param>
        /// <param name="conn">SqlConnection对象，不允许为空对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数数组</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, string spName, params object[] parameterValues)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = spName;
            cmd.Parameters.Clear();
            if (parameterValues != null)
            {
                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    cmd.Parameters[i].Value = parameterValues[i];
                }
            }
        }
        #endregion
    }
}
