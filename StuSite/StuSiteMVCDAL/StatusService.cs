using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using StuSiteMVC.Models;

namespace StuSiteMVC.DAL
{
    public class StatusService
    {
        public Status GetStatusById(int statusid)
        {
            //sql连接字符串
            string sql = "select * from Status where StatusId=@statusid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@statusid",statusid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            Status status = null;
            if (reader.Read())
            {
                status = new Status();
                status.StatusId = (int)reader["StatusId"];
                status.StatusName = (string)reader["StatusName"];
            }
            return status;
        }
    }
}
