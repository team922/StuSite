using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuSiteMVC.DAL;
using StuSiteMVC.Models;

namespace StuSiteMVC.BLL
{
    public class UserManager
    {
        //学生登录
        public bool SLogin(string LoginId, string LoginPwd, out SLogin S)
        {
            SLogin s = new SLoginService().LoginBySNumber(LoginId);

            if (s == null)
            {
                S = null;//用户名不存在
                return false;
            }
            if (s.SPassword == LoginPwd)
            {
                S = s;//登录成功
                return true;
            }
            else
            {
                S = null;//登录失败
                return false;
            }
        }

        //教师登录
        public bool TLogin(string LoginId, string LoginPwd, out TLogin T)
        {
            TLogin t = new TLoginService().LoginByTNumber(LoginId);

            if (t == null)
            {
                T = null;//用户名不存在
                return false;
            }
            if (t.TPassword == LoginPwd)
            {
                T = t;//登录成功
                return true;
            }
            else
            {
                T = null;//登录失败
                return false;
            }
        }

        //管理登录
        public bool ALogin(string LoginId,string LoginPwd, out Admin A)
        {
            Admin a = new AdminService().GetAdminByLoginId(LoginId);

            if (a == null)
            {
                A = null;//用户名不存在
                return false;
            }
            if (a.Password == LoginPwd)
            {
                A = a;//登录成功
                return true;
            }
            else
            {
                A = null;//登录失败
                return false;
            }
        }
    }
}
