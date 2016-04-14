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
    public class AdminService
    {
        //添加Admin用户
        public void AddAdmin(Admin admin)
        {
            //1.sql语句
            string sql = "insert into Admin(AdminId,Password,Name,Phone,Email,State,LastIP,LastTime)"
                         + " values(@AdminId,@Password,@Name,@Phone,@Email,@State,@LastIP,@LastTime)";
            sql += " select @@identity";
            //2.参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@AdminId",admin.AdminId),
                new SqlParameter("@Password",admin.Password),
                new SqlParameter("@Name",admin.Name),
                new SqlParameter("@Phone",admin.Phone),
                new SqlParameter("@Email",admin.Email),
                new SqlParameter("@State",admin.State.StateId),
                new SqlParameter("@LastIP",admin.LastIP),
                new SqlParameter("@LastTime",admin.LastTime),
              };
            //3、执行sql语句
            admin.id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para));
        }

        //删除Admin用户 by Id（逻辑删除）
        public bool DeleteAdminById(Admin admin)
        {
            string sql = "update Admin set State=0 where id=@id";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@id",admin.id)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para) > 0;
        }

        //删除Admin用户 by LoginId（逻辑删除）
        public bool DeleteAdminByLoginId(Admin admin)
        {
            string sql = "update Admin set State=0 where AdminId=@AdminId";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@AdminId",admin.AdminId)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para) > 0;
        }

        //修改用户 参数 admin bool
        public bool UpdataUser(Admin admin)
        {
            string sql = "update Admin set " +
                "AdminId = @AdminId," +
                "Password = @Password," +
                "Name = @Name," +
                "Phone = @Phone," +
                "Email = @Email," +
                "State = @State," +
                "where id=@id";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@id",admin.id),
                new SqlParameter("@AdminId",admin.AdminId),
                new SqlParameter("@Password",admin.Password),
                new SqlParameter("@Name",admin.Name),
                new SqlParameter("@Phone",admin.Phone),
                new SqlParameter("@Email",admin.Email),
                new SqlParameter("@State",admin.State.StateId),
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, para) > 0;
        }

        //获取admin by id
        public Admin GetAdminById(int id)
        {
            //sql语句
            string sql = "select * from Admin,State where Admin.State=State.StateId and id=@id";
            Admin admin = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)))
            {
                if (reader.Read())
                {
                    StateService stateService = new StateService();

                    admin = new Admin();
                    admin.id = (int)reader["id"];
                    admin.AdminId = (string)reader["AdminId"];
                    admin.Password = (string)reader["Password"];
                    admin.Name = (string)reader["Name"];
                    admin.Phone = (string)reader["Phone"];
                    admin.Email = (string)reader["Email"];
                    admin.State = stateService.GetStateById((int)reader["State"]);
                    admin.LastIP = (string)reader["LastIP"];
                    admin.LastTime = reader["LastTime"] != DBNull.Value ? (DateTime?)reader["LastTime"] : null;
                }
            }
            return admin;
        }

        //获取admin by LoginId
        public Admin GetAdminByLoginId(string loginid)
        {
            //sql语句
            string sql = "select * from Admin,State where Admin.State=State.StateId and Admin.AdminId=@AdminId";
            Admin admin = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@AdminId", loginid)))
            {
                if (reader.Read())
                {
                    StateService stateService = new StateService();

                    admin = new Admin();
                    admin.id = (int)reader["id"];
                    admin.AdminId = (string)reader["AdminId"];
                    admin.Password = (string)reader["Password"];
                    admin.Name = (string)reader["Name"];
                    admin.Phone = (string)reader["Phone"];
                    admin.Email = (string)reader["Email"];
                    admin.State = stateService.GetStateById((int)reader["State"]);
                    admin.LastIP = (string)reader["LastIP"];
                    admin.LastTime = reader["LastTime"] != DBNull.Value ? (DateTime?)reader["LastTime"] : null;
                }
            }
            return admin;
        }

        //获取admin by E-Mail
        public Admin GetAdminByEmail(string email)
        {
            //sql语句
            string sql = "select * from Admin,State where Admin.State=State.StateId and Admin.Email=@Email";
            Admin admin = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@AdminId", email)))
            {
                if (reader.Read())
                {
                    StateService stateService = new StateService();

                    admin = new Admin();
                    admin.id = (int)reader["id"];
                    admin.AdminId = (string)reader["AdminId"];
                    admin.Password = (string)reader["Password"];
                    admin.Name = (string)reader["Name"];
                    admin.Phone = (string)reader["Phone"];
                    admin.Email = (string)reader["Email"];
                    admin.State = stateService.GetStateById((int)reader["State"]);
                    admin.LastIP = (string)reader["LastIP"];
                    admin.LastTime = reader["LastTime"] != DBNull.Value ? (DateTime?)reader["LastTime"] : null;
                }
            }
            return admin;
        }
    }
}

