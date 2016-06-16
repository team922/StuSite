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
        public bool ALogin(string LoginId, string LoginPwd, out Admin A)
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

        //获取学生信息
        public SBasic GetStudent(string id)
        {
            return new SBasicService().GetStudentBsaicBySNumber(id);
        }

        //获取院系列表
        public List<College> GetCollege()
        {
            return new CollegeService().GetCollege();
        }

        //获取专业列表by collegeid
        public List<Major> GetMajorByCollegeid(string id)
        {
            return new MajorService().GetMajorByCollegeid(id);
        }

        //获取某专业最新插入数据
        public string SelectTheLatestData(string major, string year)
        {
            return new SBasicService().SelectTheLatestData(major, year);
        }

        //添加学生
        public string AddStudent(SLogin slogin)
        {
            string number = "0";
            if (!new SBasicService().SelectIDCardisLogin(slogin.SNumber.SIDNumber))
            {
                number = new SBasicService().AddStudent(slogin);
                new SLoginService().AddStudent(slogin);
                return number;
            }
            else
            {
                return number;
            }
        }

        //修改学生信息 （学生端-前台）
        public bool UpdateStudentBasic(string number, string phone, string email)
        {
            return new SBasicService().UpdateStudentBasic(number, phone, email);
        }

        //修改学生信息 （管理端-后台）
        public bool UpdateStudentBasic(string number, string name, string idnumber, string sex, string birthday, string address)
        {
            return new SBasicService().UpdateStudentBasic(number, name, idnumber, sex, birthday, address);
        }

        //修改用户密码（原密码）
        public bool ChangePasswordByOldPassword(string number, string oldpassword, string newpassword)
        {
            SLogin slogin = new SLoginService().LoginBySNumber(number);
            if (slogin.SPassword == oldpassword)
            {
                return new SLoginService().ChangePassword(number, newpassword);
            }
            else
            {
                return false;
            }
        }

        //修改用户密码（邮箱）
        public bool ChangePasswordByEmail(string number, string email, string newpassword)
        {
            SBasic sbasic = new SBasicService().GetStudentBsaicBySNumber(number);
            if (sbasic.SEmail == email)
            {
                return new SLoginService().ChangePassword(number, newpassword);
            }
            else
            {
                return false;
            }
        }

        //获取冻结账户
        public List<SLogin> GetLocked()
        {
            return new SLoginService().GetLocked();
        }

        //冻结用户
        public bool LockStudent(string number)
        {
            return new SLoginService().LockStudent(number);
        }

        //解冻账户
        public bool UnlockStudent(string number)
        {
            return new SLoginService().UnlockStudent(number);
        }

        //冻结指定时间范围内未登录的用户
        public bool LockStudentByMonth(string date)
        {
            return new SLoginService().LockStudentByMonth(date);
        }
    }
}
