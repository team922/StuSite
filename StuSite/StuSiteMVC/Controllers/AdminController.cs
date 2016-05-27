using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StuSiteMVC.BLL;
using StuSiteMVC.Models;

namespace StuSiteMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            try
            {
                Admin A = Session["Admin"] != null ? (Admin)Session["Admin"] : null;
                if (A == null)
                {
                    return View("../Account/Admin");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View("../Account/Admin");
            }
        }

        public ActionResult LoginChack()
        {
            if (Session["Admin"] == null)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }

        public ActionResult StuManage1()
        {
            return View();
        }

        public ActionResult StuManage2()
        {
            return View();
        }

        public ActionResult StuManage3()
        {
            return View();
        }

        public ActionResult TeaManage1()
        {
            return View();
        }

        public ActionResult TeaManage2()
        {
            return View();
        }

        public ActionResult NoticeManage1()
        {
            return View();
        }

        public ActionResult NoticeManage2()
        {
            return View();
        }

        public ActionResult NewsManage1()
        {
            return View();
        }

        public ActionResult NewsManage2()
        {
            return View();
        }

        public ActionResult AddNews()
        {
            string news_title = Request.Form["title"];//获取标题
            string news_state = Request.Form["select_text"];//获取状态
            string news_body = Server.HtmlEncode(Request.Form["ckeditor_html"]);//获取内容

            Admin A = new Admin();
            A = Session["Admin"] != null ? (Admin)Session["Admin"] : null;

            News news = new News();
            news.NewsTitle = news_title;
            news.NewsMain = news_body;
            news.NewsDate = new IPManager().GetDateTime();
            news.NewsPublisher = A;
            news.NState = new StateManager().GetNStateById(Convert.ToInt16(news_state));
            news.NewsHits = 0;

            if (new NewsManager().AddNews(news))
            {
                Response.Write("<script>alert('添加新闻成功！')</script>");
            }
            else
            {
                Response.Write("<script>alert('添加新闻失败！')</script>");
            }
            return View("NewsManage1");
        }
    }
}