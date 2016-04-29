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
    public class SLoginService
    {
        //获取登录信息 by SNumber
        public SLogin LoginBySNumber(string loginid)
        {
            //sql连接字符串
            string sql = "select * from SLogin where SNumber=@loginid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@loginid",loginid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            SLogin slogin = null;
            if (reader.Read())
            {
                SBasicService sBasicService = new SBasicService();
                StateService stateServer = new StateService();

                slogin = new SLogin();
                slogin.id = (int)reader["id"];
                slogin.SNumber = sBasicService.GetStudentBsaicBySNumber((string)reader["SNumber"]);
                slogin.SPassword = (string)reader["SPassword"];
                slogin.State = stateServer.GetStateById((int)reader["State"]);
                slogin.LastIP = reader["LastIP"] != DBNull.Value ? (string)reader["LastIP"] : null;
                slogin.LastTime = reader["LastTime"] != DBNull.Value ? (DateTime?)reader["LastTime"] : null;
            }
            return slogin;
        }

        //更新登录信息(IP and Time)
        public bool UpsetSLoginIPandTime(string loginid)
        {
            IPService getip = new IPService();
            string IP = getip.GetIP();
            DateTime datetime = getip.GetDateTime();
            string sql = "update SLogin set LastIP=@lastip,LastTime=@lasttime where SNumber=@loginid";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@loginid",loginid),
                new SqlParameter("@lastip",IP),
                new SqlParameter("@lasttime",datetime),
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para) > 0;
        }
    }
}
