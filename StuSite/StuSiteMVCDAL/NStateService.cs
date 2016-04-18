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
    public class NStateService
    {
        //获取新闻、公告状态信息 by nstateid
        public NState GetNStateById(int nstateid)
        {
            //sql连接字符串
            string sql = "select * from NState where NStateId=@nstateid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@nstateid",nstateid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            NState nstate = null;
            if (reader.Read())
            {
                nstate = new NState();
                nstate.NStateId = (int)reader["NStateId"];
                nstate.NStateName = (string)reader["NStateName"];
            }
            return nstate;
        }
    }
}
