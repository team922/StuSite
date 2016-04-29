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
    public class CollegeService
    {
        //获取学院信息 by collegeid
        public College GetCollegeByCollegeId(string collegeid)
        {
            //sql连接字符串
            string sql = "select * from College where CollegeId=@collegeid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@collegeid",collegeid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            College college = null;
            if (reader.Read())
            {
                college = new College();
                college.CollegeId = (string)reader["CollegeId"];
                college.CollegeName = (string)reader["CollegeName"];
            }
            return college;
        }
    }
}
