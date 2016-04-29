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
    public class MajorService
    {
        //获取专业信息 by majorid
        public Major GetMajorByMajorId(int majorid)
        {
            //sql连接字符串
            string sql = "select * from Major,College where Major.Belong=College.CollegeId and MajorId=@majorid";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@majorid",majorid),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            Major major = null;
            if (reader.Read())
            {
                CollegeService collegeService = new CollegeService();

                major = new Major();
                major.MajorId = (string)reader["MajorId"];
                major.MajorName = (string)reader["MajorName"];
                major.Belong = collegeService.GetCollegeByCollegeId((string)reader["Belong"]);
            }
            return major;
        }
    }
}
