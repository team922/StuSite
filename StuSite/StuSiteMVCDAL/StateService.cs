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
    public class StateService
    {
        //获取状态信息 by stateid
        public State GetStateById(int stateid)
        {
            //sql连接字符串
            string sql = "select * from State where StateId=@stateid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@stateid",stateid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            State state = null;
            if (reader.Read())
            {
                state = new State();
                state.StateId = (int)reader["StateId"];
                state.StateName = (string)reader["StateName"];
            }
            return state;
        }
    }
}
