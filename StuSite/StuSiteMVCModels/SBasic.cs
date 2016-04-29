using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class SBasic
    {
        //学生基本信息表
        public string SNumber { get; set; }            //学生学号
        public string SName { get; set; }              //学生姓名
        public string SIDNumber { get; set; }          //学生身份证号
        public College SCollege { get; set; }          //学生所属学院
        public Major SMajor { get; set; }              //学生所属专业
        public DateTime? SErollment { get; set; }      //学生入学日期
        public string SPhone { get; set; }             //学生电话
        public string SEmail { get; set; }             //学生E-Mail
        public DateTime? SBirthday { get; set; }       //学生出生日期
        public string SAddress { get; set; }           //学生家庭住址
        public string SPicAddress { get; set; }        //学生个人照片存放地址
    }
}
