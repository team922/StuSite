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

        //ajax
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

        public ActionResult GetDepartment()
        {
            List<Department> departmentlist = new List<Department>();
            departmentlist = new NoticeManager().GetAllDepartment();

            int i = 0;
            string json = "{\"departmentlist\":[";
            foreach (var department in departmentlist)
            {
                i++;
                json += "{";
                json += "\"id\":\"" + department.Did + "\",";
                json += "\"name\":\"" + department.DName + "\"";
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "],\"number\":\"" + i + "\"}";
            return Content(json);
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

        public ActionResult AddNotice()
        {
            string notice_title = Request.Form["title"];//获取标题
            string notice_belong = Request.Form["select1_text"];//获取分类
            string notice_state = Request.Form["select2_text"];//获取状态
            string notice_main = Server.HtmlEncode(Request.Form["ckeditor_html"]);//获取内容

            Admin A = new Admin();
            A = Session["Admin"] != null ? (Admin)Session["Admin"] : null;

            Notices notice = new Notices();
            notice.NoticeTitle = notice_title;
            notice.NoticeMain = notice_main;
            notice.NoticeDate = new IPManager().GetDateTime();
            notice.NoticePublisher = A;
            notice.NoticeBelong = new NoticeManager().GetDepartment(Convert.ToInt16(notice_belong));
            notice.NState = new StateManager().GetNStateById(Convert.ToInt16(notice_state));
            notice.NoticeHits = 0;

            if (new NoticeManager().AddNotice(notice))
            {
                Response.Write("<script>alert('添加公告成功！')</script>");
            }
            else
            {
                Response.Write("<script>alert('添加公告失败！')</script>");
            }
            return View("NoticeManage1");
        }
    }
}