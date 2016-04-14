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
                tBasic.TNumber = (string)reader["SNumber"];
                tBasic.TName = (string)reader["SName"];
                tBasic.TIDNumber = (string)reader["SIDNumber"];
                tBasic.TCollede = collegeService.GetCollegeByCollegeId((int)reader["TCollede"]);
                tBasic.TPhone = (string)reader["SPhone"];
                tBasic.TEmail = (string)reader["SEmail"];
                tBasic.TBirthday = reader["TBirthday"] != DBNull.Value ? (DateTime?)reader["TBirthday"] : null;
                tBasic.TAddress = (string)reader["SAddress"];
                tBasic.TPicture = (string)reader["SPicture"];
            }
            return tBasic;
        }
    }
}
