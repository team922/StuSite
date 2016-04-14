using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class TBasic
    {
        //教师基本信息表
        public string TNumber { get; set; }            //教师编号
        public string TName { get; set; }              //教师姓名
        public string TIDNumber { get; set; }          //教师身份证号
        public College TCollede { get; set; }          //教师所属学院
        public string TPhone { get; set; }             //教师电话
        public string TEmail { get; set; }             //教师E-Mail
        public DateTime? TBirthday { get; set; }       //教师出生日期
        public string TAddress { get; set; }           //教师家庭住址
        public string TPicture { get; set; }           //教师个人照片存放地址
    }
}
