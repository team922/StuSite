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
    public class DepartmentService
    {
        //获取部门信息
        public Department GetDepartmentById(int departmentid)
        {
            //sql连接字符串
            string sql = "select * from Department where Did=@departmentid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@departmentid",departmentid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            Department department = null;
            if (reader.Read())
            {
                department = new Department();
                department.Did = (int)reader["Did"];
                department.DName = (string)reader["DName"];
            }
            return department;
        }
    }
}
