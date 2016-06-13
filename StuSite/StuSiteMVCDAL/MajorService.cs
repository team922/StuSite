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
        public Major GetMajorByMajorId(string majorid)
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


        //获取专业信息by collegeid
        public List<Major> GetMajorByCollegeid(string id)
        {
            List<Major> majorlist = new List<Major>();
            //sql连接字符串
            string sql = "select * from Major where Belong=@collegeid";
            //执行sql
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@collegeid", id),
            };
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql,para);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Major major = new Major();
                    major.MajorId = (string)row["MajorId"];
                    major.MajorName = (string)row["MajorName"];

                    majorlist.Add(major);
                }
            }
            return majorlist;
        }
    }
}
