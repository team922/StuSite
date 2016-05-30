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
            return new IPService().GetIPAddress();
        }

        //获取地址
        public string GetAddress()
        {
            return new IPService().GetAddress(GetIP());
        }

        //获取地区代码
        public string GetAreaid(string district)
        {
            return new IPService().GetAreaid(district);
        }

        //获取天气
        public string GetWeather(string id)
        {
            return new IPService().GetWeather(id);
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

        //生成验证码
        public string CreateRandomCode(int codeCount)
        {
            return new IPService().CreateRandomCode(codeCount);
        }

        //生成图片
        public byte[] CreateValidateGraphic(string validateCode)
        {
            return new IPService().CreateValidateGraphic(validateCode);
        }
    }
}
