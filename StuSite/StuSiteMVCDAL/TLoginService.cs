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
    public class TLoginService
    {
        //获取登录信息 by SNumber
        public TLogin LoginByTNumber(string loginid)
        {
            //sql连接字符串
            string sql = "select * from TLogin where TNumber=@loginid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@loginid",loginid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            TLogin tlogin = null;
            if (reader.Read())
            {
                TBasicService tBasicService = new TBasicService();
                StateService stateServer = new StateService();
                RoleService roleServer = new RoleService();

                tlogin = new TLogin();
                tlogin.id = (int)reader["id"];
                tlogin.TNumber = tBasicService.GetTeacherBsaicByTNumber((string)reader["TNumber"]);
                tlogin.TPassword = (string)reader["TPassword"];
                tlogin.State = stateServer.GetStateById((int)reader["State"]);
                tlogin.Role = roleServer.GetRoleById((int)reader["Role"]);
                tlogin.LastIP = reader["LastIP"] != DBNull.Value ? (string)reader["LastIP"] : null;
                tlogin.LastTime = reader["LastTime"] != DBNull.Value ? (DateTime?)reader["LastTime"] : null;
            }
            return tlogin;
        }

        //更新登录信息(IP and Time)
        public bool UpsetTLoginIPandTime(string loginid)
        {
            IPService getip = new IPService();
            string IP = getip.GetIP();
            DateTime datetime = getip.GetDateTime();
            string sql = "update TLogin set LastIP=@lastip,LastTime=@lasttime where TNumber=@loginid";
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
