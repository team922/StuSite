using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuSiteMVC.DAL;
using StuSiteMVC.Models;

namespace StuSiteMVC.BLL
{
    public class IPManager
    {
        //获取IP
        public string GetIP()
        {
            return new IPService().GetIP();
        }

        //获取省份
        public string GetAddress()
        {
            return new IPService().GetAddress();
        }

        //获取运营商
        public string GetProvider()
        {
            return new IPService().GetProvider();
        }

        //获取系统时间
        public DateTime GetDateTime()
        {
            return new IPService().GetDateTime();
        }

        //添加登录IP、时间（学生）
        public void AddSIP(string loginid)
        {
            new SLoginService().UpsetSLoginIPandTime(loginid, GetIP(), GetDateTime());
        }

        //添加登录IP、时间（教师）
        public void AddTIP(string loginid)
        {
            new TLoginService().UpsetTLoginIPandTime(loginid, GetIP(), GetDateTime());
        }

        //添加登录IP、时间（管理）
        public void AddAIP(string loginid)
        {
            new AdminService().UpsetAdminIPandTime(loginid, GetIP(), GetDateTime());
        }
    }
}
