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
                sBasic.SEnrollment = reader["SEnrollment"] != DBNull.Value ? (DateTime?)reader["SEnrollment"] : null;
                sBasic.SStatus = status.GetStatusById((int)reader["SStatus"]);
                sBasic.SPhone = (string)reader["SPhone"];
                sBasic.SEmail = reader["SEmail"].ToString();
                sBasic.SBirthday = reader["SBirthday"] != DBNull.Value ? (DateTime?)reader["SBirthday"] : null;
                sBasic.SAddress = (string)reader["SAddress"];
                sBasic.SPicAddress = (string)reader["SPicAddress"];
            }
            return sBasic;
        }

        //获取某专业最新插入数据
        public string SelectTheLatestData(string major, string year)
        {
            string number = year + major.Trim() + "___";
            string sql = "select top 1 SNumber from SBasic where SNumber like @number";
            //执行sql
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@number", number));

            if (reader.Read())
            {
                return (string)reader["SNumber"];
            }
            else
            {
                return "0";
            }
        }

        //查询身份证号是否已被注册
        public bool SelectIDCardisLogin(string idcard)
        {
            string sql = "select * from SBasic where SIDNumber=@idnumber";
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@idnumber", idcard));
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //添加学生信息
        public string AddStudent(SLogin slogin)
        {
            //1.sql语句
            string sql = "insert into SBasic(SNumber,SName,SIDNumber,SCollege,SMajor,SEnrollment,SStatus,SSex,SPhone,SBirthday,SAddress,SPicAddress)"
                         + " values(@SNumber,@SName,@SIDNumber,@SCollege,@SMajor,@SEnrollment,@SStatus,@SSex,@SPhone,@SBirthday,@SAddress,@SPicAddress)";
            sql += " select @@identity";
            //2.参数赋值
            SBasic sbasic = new SBasic();
            sbasic = slogin.SNumber;

            College college = new College();
            college = sbasic.SCollege;
            Major major = new Major();
            major = sbasic.SMajor;
            Status status = new Status();
            status = sbasic.SStatus;
            State state = new State();
            state = slogin.State;

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@SNumber",sbasic.SNumber),
                new SqlParameter("@SName",sbasic.SName),
                new SqlParameter("@SIDNumber",sbasic.SIDNumber),
                new SqlParameter("@SCollege",college.CollegeId),
                new SqlParameter("@SMajor",major.MajorId),
                new SqlParameter("@SEnrollment",sbasic.SEnrollment),
                new SqlParameter("@SStatus",status.StatusId),
                new SqlParameter("@SSex",sbasic.SSex),
                new SqlParameter("@SPhone",sbasic.SPhone),
                new SqlParameter("@SBirthday",sbasic.SBirthday),
                new SqlParameter("@SAddress",sbasic.SAddress),
                new SqlParameter("@SPicAddress",sbasic.SPicAddress),
              };
            //3、执行sql语句
            SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para);
            return sbasic.SNumber;
        }
    }
}
