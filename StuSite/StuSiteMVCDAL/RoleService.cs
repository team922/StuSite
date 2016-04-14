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
    public class RoleService
    {
        //获取权限信息 by roleid
        public Role GetRoleById(int roleid)
        {
            //sql连接字符串
            string sql = "select * from Role where RoleId=@roleid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@roleid",roleid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            Role role = null;
            if (reader.Read())
            {
                role = new Role();
                role.RoleId = (int)reader["RoleId"];
                role.RoleName = (string)reader["RoleName"];
            }
            return role;
        }
    }
}
