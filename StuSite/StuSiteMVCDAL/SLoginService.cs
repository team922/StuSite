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
        public bool UpsetSLoginIPandTime(string loginid, string IP, DateTime datetime)
        {
            string sql = "update SLogin set LastIP=@lastip,LastTime=@lasttime where SNumber=@loginid";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@loginid",loginid),
                new SqlParameter("@lastip",IP),
                new SqlParameter("@lasttime",datetime),
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para) > 0;
        }

        //添加学生用户登录信息
        public bool AddStudent(SLogin slogin)
        { //1.sql语句
            string sql = "insert into SLogin(SNumber,SPassword,State,LastIP,LastTime)"
                         + " values(@SNumber,@SPassword,@State,@LastIP,@LastTime)";
            //2.参数赋值
            SBasic sbasic = new SBasic();
            sbasic = slogin.SNumber;
            State state = new State();
            state = slogin.State;

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@SNumber",sbasic.SNumber),
                new SqlParameter("@SPassword",slogin.SPassword),
                new SqlParameter("@State",state.StateId),
                new SqlParameter("@LastIP","192.168.1.1"),
                new SqlParameter("@LastTime","1900-01-01 08:00:00"),
              };
            //3、执行sql语句
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para)) > 0;
        }

        //修改学生用户登录信息
        public bool ChangePassword(string number, string password)
        {
            string sql = "update SLogin set SPassword=@password where SNumber=@number";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("password",password),
                new SqlParameter("number",number),
            };
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para)) == 1;
        }

        //获取冻结用户信息
        public List<SLogin> GetLocked()
        {
            List<SLogin> sloginlist = new List<SLogin>();
            string sql = "select * from SLogin,SBasic where SLogin.SNumber=SBasic.SNumber and SLogin.State=0";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    SLogin slogin = new SLogin();
                    SBasic sbasic = new SBasic();
                    sbasic.SNumber= (string)row["SNumber"];
                    slogin.SNumber = sbasic;
                    slogin.SPassword = (string)row["SName"];
                    /*此时spassword代表姓名数据*/
                    sloginlist.Add(slogin);
                }
            }
            return sloginlist;
        }

        //冻结用户账户
        public bool LockStudent(string number)
        {
            string sql = "update SLogin set State=0 where Snumber=@number";
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("number", number))) > 0;
        }

        //解冻用户账户
        public bool UnlockStudent(string number)
        {
            string sql = "update SLogin set State=1 where SNumber=@number";
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("number", number))) > 0;
        }

        //冻结用户（指定日期内未登录）
        public bool LockStudentByMonth(string date)
        {
            string sql = "update SLogin set state=0  where LastTime <=@date";
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@date", date))) > 0;
        }
    }
}
