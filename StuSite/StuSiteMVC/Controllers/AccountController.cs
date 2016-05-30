﻿using System;
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

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return View("Index");
        }
        
        public ActionResult GetRandomCode()
        {
            string oldcode = Session["SecurityCode"] as string;
            string code = new IPManager().CreateRandomCode(5);
            Session["SecurityCode"] = code;
            return File(new IPManager().CreateValidateGraphic(code), "image/jpeg");
        }

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