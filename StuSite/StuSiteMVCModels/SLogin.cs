using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class SLogin
    {
        //学生登录信息表
        public int id { get; set; }                //学生自增ID
        public SBasic SNumber { get; set; }        //学生学号（登录名）
        public string SPassword { get; set; }      //学生登录密码
        public State State { get; set; }           //学生状态
        public string LastIP { get; set; }         //学生上次登录IP
        public DateTime? LastTime { get; set; }    //学生上次登录时间
    }
}
