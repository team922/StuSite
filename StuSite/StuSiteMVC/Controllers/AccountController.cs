using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
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

            if (Regex.IsMatch(loginid, @"^t"))
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

        private string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random random = new Random();
            for(int i=0; i < codeCount; i++)
            {
                if(temp!=-1)
                {
                    random = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = random.Next(36);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 30);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                //1、画验证码干扰线【背景】
                for(int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Width);
                    int y2 = random.Next(image.Width);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                //2、画验证码干扰点【前景】
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //3、验证的字符串画到图片
                Font font = new Font("Arial", 13, (FontStyle.Bold));
                LinearGradientBrush burth = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Red, 1.2f, true);
                g.DrawString(validateCode, font, burth, 3, 2);
                //4、画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width-1, image.Height-1);
                //5、保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //6、输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public ActionResult SecurityCode()
        {
            string oldcode = TempData["SecurityCode"] as string;
            string code = CreateRandomCode(5);
            TempData["SecurityCode"] = code;
            return File(CreateValidateGraphic(code), "image/jpeg");
        }
    }
}