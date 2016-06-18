using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using StuSiteMVC.BLL;
using StuSiteMVC.Models;

namespace StuSiteMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        /*Account/Index(用户登录界面)
        1、检测登录状态（session）
        2、未登录跳转到Account/Index（用户登录界面）
        3、已登录跳转到Home/Index（主界面）*/
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            else
            {
                return View("../Home/Index");
            }
        }

        /*Account/Index（管理员登录界面）
        1、检测管理员登录状态
        2、未登录跳转到Account/Admin（管理员登录界面）
        3、已登录跳转到Admin/Index（管理员主界面）*/
        public ActionResult Admin()
        {
            if (Convert.ToString(Session["Admin"]) == "")
            {
                return View();
            }
            else
            {
                return View("../Admin/Index");
            }
        }

        /*Account/UserLogin（用户登录事件）
        1、获取界面信息（用户名、密码、是否记住密码-cookie）
        2、检测登录用户分类（学生11位纯数字--教师t+9位纯数字）
        3、执行登录过程（业务逻辑层UserManager）
        4、学生-UserManager-SLogin
        5、教师-UserManager-Tlogin
        6、返回登录信息
        7、成功后跳转到Home/Index（主界面）*/
        public ActionResult UserLogin()
        {
            string loginid = Request.Form["number"];//获取用户名
            string loginpwd = Request.Form["password"];//获取密码
            string recordMe = Request.Form["RecordMe"];//是否记住密码
            bool record = false;

            if (!string.IsNullOrEmpty(recordMe))
            {
                record = true;
            }

            if (Regex.IsMatch(loginid, @"^t|T"))
            {
                #region 教师登录
                TLogin T = new TLogin();
                if (new UserManager().TLogin(loginid, loginpwd, out T))
                {
                    if (record)
                    {
                        //添加cookie,设置用户过期。
                        HttpCookie idCookie = new HttpCookie("id", T.TNumber.TNumber);
                        idCookie.Expires = DateTime.MaxValue;
                        Response.Cookies.Add(idCookie);
                        HttpCookie pwdCookie = new HttpCookie("pwd", T.TPassword);
                        pwdCookie.Expires = DateTime.MaxValue;
                        Response.Cookies.Add(pwdCookie);
                    }
                    else
                    {
                        //删除cookie（失效）
                        Response.Cookies["id"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                    }
                    if (T.State.StateName == "正常")
                    {
                        //保存用户的状态（sesion）
                        Session["user"] = T;

                        new IPManager().AddTIP(T.TNumber.TNumber);

                        return View("../Home/Index");
                    }
                    else
                    {
                        Response.Write("<script>alert('你的账号已被冻结，请联系管理员！')</script>");
                        return View("../Account/Index");
                    }
                }
                else
                {
                    Response.Write("<script>alert('用户名与密码不匹配，请重试！')</script>");
                    return View("../Account/Index");
                }
                #endregion
            }
            else
            {
                #region 学生登录
                SLogin S = new SLogin();
                if (new UserManager().SLogin(loginid, loginpwd, out S))
                {
                    if (record)
                    {
                        //添加cookie,设置用户过期。
                        HttpCookie idCookie = new HttpCookie("id", S.SNumber.SNumber);
                        idCookie.Expires = DateTime.MaxValue;
                        Response.Cookies.Add(idCookie);
                        HttpCookie pwdCookie = new HttpCookie("pwd", S.SPassword);
                        pwdCookie.Expires = DateTime.MaxValue;
                        Response.Cookies.Add(pwdCookie);
                    }
                    else
                    {
                        //删除cookie（失效）
                        Response.Cookies["id"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                    }
                    if (S.State.StateName == "正常")
                    {
                        //保存用户的状态（sesion）
                        Session["user"] = S;

                        new IPManager().AddSIP(S.SNumber.SNumber);

                        return View("../Home/Index");
                    }
                    else
                    {
                        Response.Write("<script>alert('你的账号已被冻结，请联系管理员！')</script>");
                        return View("../Account/Index");
                    }
                }
                else
                {
                    Response.Write("<script>alert('用户名与密码不匹配，请重试！')</script>");
                    return View("../Account/Index");
                }
                #endregion
            }
        }

        /*Account/AdminLogin（管理员登录事件）
        1、获取登录信息（用户名、密码）
        2、执行登录过程（业务逻辑层UserManager-AdminLogin）
        3、返回登录信息
        4、跳转到Admin/Index（管理系统主界面）*/
        public ActionResult AdminLogin()
        {
            string adminid = Request.Form["adminid"];//获取用户名
            string adminpwd = Request.Form["adminpwd"];//获取密码

            Admin A = new Admin();
            if (new UserManager().ALogin(adminid, adminpwd, out A))
            {
                if (A.State.StateName == "正常")
                {
                    //保存用户的状态（sesion）
                    Session["Admin"] = A;

                    new IPManager().AddAIP(A.AdminId);

                    return View("../Admin/Index");
                }
                else
                {
                    Response.Write("<script>alert('你的账号已被冻结，请联系管理员！')</script>");
                    return View("../Account/Admin");
                }
            }
            else
            {
                Response.Write("<script>alert('用户名与密码不匹配，请重试！')</script>");
                return View("../Account/Admin");
            }
        }

        /*Account/Logout（退出登录事件）
        1、清空session
        2、返回登录界面*/
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Index");
        }

        /*Account/GetRandomCode（获取随机数字-验证码图片）
        1、访问IPManager-CreateRandomCode获取验证码数字--参数为验证码长度
        2、通过IPManager-CreateValidateGraphic生成验证码图片--参数为验证码字符串
        3、返回图片地址*/
        public ActionResult GetRandomCode()
        {
            string oldcode = Session["SecurityCode"] as string;
            string code = new IPManager().CreateRandomCode(5);
            Session["SecurityCode"] = code;
            return File(new IPManager().CreateValidateGraphic(code), "image/jpeg");
        }

        /*Account/ChackRandomCode（验证验证码是否正确）*/
        public ActionResult ChackRandomCode(string code)
        {
            string realcode = Session["SecurityCode"] as string;
            if (realcode==code.ToUpper())
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }
    }
}