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
        //获取全部部门
        public List<Department> GetDepartment()
        {
            List<Department> departmentlist = new List<Department>();
            string sql = "select * from Department";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Department department = new Department();
                    department.Did = (int)row["Did"];
                    department.DName = (string)row["DName"];

                    departmentlist.Add(department);
                }
            }
            return departmentlist;
        }

        //获取部门信息byid
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
