using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class Admin
    {
        //管理员信息表
        public int id { get; set; }              //自增id
        public string AdminId { get; set; }      //管理员ID
        public string Password { get; set; }     //管理员密码
        public string Name { get; set; }         //管理员姓名
        public string Phone { get; set; }        //管理员电话
        public string Email { get; set; }        //管理员E-Mail
        public State State { get; set; }         //管理员状态
        public string LastIP { get; set; }       //管理员上次登录IP
        public DateTime? LastTime { get; set; }  //管理员上次登录时间
    }
}
