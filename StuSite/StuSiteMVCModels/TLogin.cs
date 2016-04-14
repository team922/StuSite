using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class TLogin
    {
        //教师登录信息表
        public int id { get; set; }              //教师自增ID
        public TBasic TNumber { get; set; }      //教师编号（登录名-以T开始）
        public string TPassword { get; set; }    //教师登录密码
        public State State { get; set; }         //教师状态
        public Role Role { get; set; }           //教师权限等级
        public string LastIP { get; set; }       //教师上次登录IP
        public DateTime? LastTime { get; set; }  //教师上次登录时间
    }
}
