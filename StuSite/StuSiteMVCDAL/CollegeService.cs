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

        //获取全部学院信息
        public List<College> GetCollege()
        {
            List<College> collegelist = new List<College>();
            //sql连接字符串
            string sql = "select * from College";
            //执行sql
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    College college = new College();
                    college.CollegeId = (string)row["CollegeId"];
                    college.CollegeName = (string)row["CollegeName"];

                    collegelist.Add(college);
                }
            }
            return collegelist;
        }
    }
}
