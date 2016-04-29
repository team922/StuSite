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
    public class TBasicService
    {
        //获取教师基本信息
        public TBasic GetTeacherBsaicByTNumber(string tnumber)
        {
            //sql连接字符串
            string sql = "select * from TBasic where TNumber=@tnumber";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@tnumber",tnumber),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            TBasic tBasic = null;
            if (reader.Read())
            {
                CollegeService collegeService = new CollegeService();

                tBasic = new TBasic();
                tBasic.TNumber = (string)reader["TNumber"];
                tBasic.TName = (string)reader["TName"];
                tBasic.TIDNumber = (string)reader["TIDNumber"];
                tBasic.TCollege = collegeService.GetCollegeByCollegeId((string)reader["TCollege"]);
                tBasic.TPhone = (string)reader["TPhone"];
                tBasic.TEmail = (string)reader["TEmail"];
                tBasic.TBirthday = reader["TBirthday"] != DBNull.Value ? (DateTime?)reader["TBirthday"] : null;
                tBasic.TAddress = (string)reader["TAddress"];
                tBasic.TPicAddress = (string)reader["TPicAddress"];
            }
            return tBasic;
        }
    }
}
