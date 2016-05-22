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
    public class SBasicService
    {
        //获取学生基本信息
        public SBasic GetStudentBsaicBySNumber(string snumber)
        {
            //sql连接字符串
            string sql = "select * from SBasic where SNumber=@snumber";
            //参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@snumber",snumber),
            };
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, para);
            SBasic sBasic = null;
            if (reader.Read())
            {
                StatusService status = new StatusService();
                CollegeService collegeService = new CollegeService();
                MajorService majorService = new MajorService();

                sBasic = new SBasic();
                sBasic.SNumber = (string)reader["SNumber"];
                sBasic.SName = (string)reader["SName"];
                sBasic.SIDNumber = (string)reader["SIDNumber"];
                sBasic.SCollege = collegeService.GetCollegeByCollegeId((string)reader["SCollege"]);
                sBasic.SMajor = majorService.GetMajorByMajorId((string)reader["SMajor"]);
                sBasic.SErollment = reader["SErollment"] != DBNull.Value ? (DateTime?)reader["SErollment"] : null;
                sBasic.SStatus = status.GetStatusById((int)reader["SStatus"]);
                sBasic.SPhone = (string)reader["SPhone"];
                sBasic.SEmail = (string)reader["SEmail"];
                sBasic.SBirthday = reader["SBirthday"] != DBNull.Value ? (DateTime?)reader["SBirthday"] : null;
                sBasic.SAddress = (string)reader["SAddress"];
                sBasic.SPicAddress = (string)reader["SPicAddress"];
            }
            return sBasic;
        }
    }
}
