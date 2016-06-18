using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StuSiteMVC.BLL;
using StuSiteMVC.Models;

namespace StuSiteMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // Get: ShowDetail
        public ActionResult ShowDetail()
        {
            return View();
        }

        // Get: ShowList
        public ActionResult ShowList()
        {
            return View();
        }

        // Get: ShowInfo
        public ActionResult ShowInfo()
        {
            return View();
        }

        // Get: Password（change）
        public ActionResult Password()
        {
            return View();
        }

        //ajax
        /*Home/LoginChack
        1、登录检测
        1.1、成功-转到目标操作
        1.2、失败-跳转到登录界面*/
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

        /*Home/GetIPMessage
        获取IP信息（api）*/
        public ActionResult GetIPMessage()
        {
            return Content(new IPManager().GetAddress());
        }

        /*Home/GetAreaid
        获取地区代码（api）*/
        public ActionResult GetAreaid(string district)
        {
            return Content(new IPManager().GetAreaid(district));
        }

        /*Home/Getweather
        获取天气信息（api）*/
        public ActionResult GetWeather(string id)
        {
            return Content(new IPManager().GetWeather(id));
        }

        /*Home/GetDepartment
        1、获取公告分类
        2、返回信息*/
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

        /*Home/GetTopNotice
        1、获取置顶公告信息
        2、不存在返回失败
        3、存在返回信息（json）*/
        public ActionResult GetTopNotice()
        {
            //json += "\"name\":\"name\",";
            Notices notice = new Notices();
            notice = new NoticeManager().GetTopNotice();

            if (notice == null)
            {
                return Content("false");
            }
            else
            {
                string json = "{";

                json += "\"id\":\"" + notice.id + "\",";
                json += "\"title\":\"" + notice.NoticeTitle + "\",";
                //json += "\"main\":\"" + notice.NoticeMain + "\",";
                json += "\"date\":\"" + Convert.ToDateTime(notice.NoticeDate).ToString("yyyy-MM-dd") + "\",";
                json += "\"publisher\":\"" + notice.NoticePublisher.Name + "\",";
                json += "\"belong\":\"" + notice.NoticeBelong.DName + "\",";
                json += "\"hits\":\"" + notice.NoticeHits + "\"";

                json += "}";
                return Content(json);
            }
        }

        /*Home/GetSelectNotice
        1、获取被选中的分类信息
        2、返回信息内容（json）*/
        public ActionResult GetSelectNotice(string department)
        {
            int id = Convert.ToInt16(department);
            List<Notices> listnotice = new List<Notices>();
            if (id == 0)
            {
                listnotice = new NoticeManager().GetTop10Notice();
            }
            else
            {
                listnotice = new NoticeManager().GetSelectNotice(id);
            }

            if (listnotice == null)
            {
                return Content("false");
            }
            else
            {
                int i = 0;
                string json = "{\"listnotice\":[";
                foreach (var notice in listnotice)
                {
                    i++;
                    json += "{";
                    json += "\"id\":\"" + notice.id + "\",";
                    json += "\"title\":\"" + notice.NoticeTitle + "\",";
                    //json += "\"main\":\"" + notice.NoticeMain + "\",";
                    json += "\"date\":\"" + Convert.ToDateTime(notice.NoticeDate).ToString("yyyy-MM-dd") + "\",";
                    json += "\"publisher\":\"" + notice.NoticePublisher.Name + "\",";
                    json += "\"belong\":\"" + notice.NoticeBelong.DName + "\",";
                    json += "\"hits\":\"" + notice.NoticeHits + "\"";
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                if (i == 0)
                {
                    json += "\"\",\"number\":\"" + i + "\"}";
                }
                else
                {
                    json += "],\"number\":\"" + i + "\"}";
                }
                return Content(json);
            }
        }

        /*Home/GetTopNews
        1、获取置顶新闻信息
        2、返回新闻内容（json）*/
        public ActionResult GetTopNews()
        {
            //json += "\"name\":\"name\",";
            News news = new News();
            news = new NewsManager().GetTopNews();

            if (news == null)
            {
                return Content("false");
            }
            else
            {
                string json = "{";

                json += "\"id\":\"" + news.id + "\",";
                json += "\"title\":\"" + news.NewsTitle + "\",";
                //json += "\"main\":\"" + news.NewsMain + "\",";
                json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToString("yyyy-MM-dd") + "\",";
                json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                json += "\"hits\":\"" + news.NewsHits + "\"";

                json += "}";
                return Content(json);
            }
        }

        /*Home/GetNews
        1、获取新闻信息（top10）
        2、返回信息（json）*/
        public ActionResult GetNews()
        {
            //json += "\"name\":\"name\",";
            List<News> listnews = new List<News>();
            listnews = new NewsManager().GetTop10News();

            if (listnews == null)
            {
                return Content("false");
            }
            else
            {
                int i = 0;
                string json = "{\"listnews\":[";
                foreach (var news in listnews)
                {
                    i++;
                    json += "{";
                    json += "\"id\":\"" + news.id + "\",";
                    json += "\"title\":\"" + news.NewsTitle + "\",";
                    //json += "\"main\":\"" + news.NewsMain + "\",";
                    json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToString("yyyy-MM-dd") + "\",";
                    json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                    json += "\"hits\":\"" + news.NewsHits + "\"";
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                if (i == 0)
                {
                    json += "\"\",\"number\":\"" + i + "\"}";
                }
                else
                {
                    json += "],\"number\":\"" + i + "\"}";
                }
                return Content(json);
            }
        }

        /*Home/ShowAddList
        1、根据url获取分页的列表信息（存储过程）
        2、获取指定分类、页数的信息
        3、返回信息（json）*/
        public ActionResult ShowAllList(string type, int pagesize, int pageindex)
        {
            int pagecount = 0;
            int datacount = 0;
            int i = 0;
            string json = "{";

            if (type == "notice")
            {
                List<Notices> listnotice = new List<Notices>();
                listnotice = new NoticeManager().GetNoticeByPage(pagesize, pageindex, ref pagecount, datacount);

                json += "\"type\":\"notice\",";
                json += "\"listnotice\":[";
                foreach (var notice in listnotice)
                {
                    i++;
                    json += "{";
                    json += "\"id\":\"" + notice.id + "\",";
                    json += "\"title\":\"" + notice.NoticeTitle + "\",";
                    //json += "\"main\":\"" + notice.NoticeMain + "\",";
                    json += "\"date\":\"" + Convert.ToDateTime(notice.NoticeDate).ToString("yyyy-MM-dd") + "\",";
                    //json += "\"publisher\":\"" + notice.NoticePublisher.Name + "\",";
                    json += "\"belong\":\"" + notice.NoticeBelong.DName + "\",";
                    json += "\"hits\":\"" + notice.NoticeHits + "\"";
                    json += "},";
                }
            }
            else
            {
                List<News> listnews = new List<News>();
                listnews = new NewsManager().GetNewsByPage(pagesize, pageindex, ref pagecount, datacount);

                json += "\"type\":\"news\",";
                json += "\"listnews\":[";
                foreach (var news in listnews)
                {
                    i++;
                    json += "{";
                    json += "\"id\":\"" + news.id + "\",";
                    json += "\"title\":\"" + news.NewsTitle + "\",";
                    //json += "\"main\":\"" + news.NewsMain + "\",";
                    json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToString("yyyy-MM-dd") + "\",";
                    json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                    json += "\"hits\":\"" + news.NewsHits + "\"";
                    json += "},";
                }
            }

            json = json.Substring(0, json.Length - 1);
            json += "],\"number\":\"" + i + "\",";

            json += "\"datacount\":\"" + datacount + "\",";
            json += "\"pagesize\":\"" + pagesize + "\",";
            json += "\"pagecount\":\"" + pagecount + "\",";
            json += "\"pageindex\":\"" + pageindex + "\"}";

            return Content(json);
        }

        /*Home/ShowSelectMessage
        1、获取要显示信息的类型、id
        2、返回信息（json--内容为编码状态-encodeURIComponent）*/
        public ActionResult ShowSelectMessage(string kind, int id)
        {
            if (kind == "notice_title")
            {
                Notices notice = new Notices();
                notice = new NoticeManager().GetNoticeById(id);
                new NoticeManager().AddNoticeHits(id);

                string json = "{\"kind\":\"notice\",";

                json += "\"id\":\"" + notice.id + "\",";
                json += "\"title\":\"" + notice.NoticeTitle + "\",";
                json += "\"main\":\"" + notice.NoticeMain + "\",";
                json += "\"date\":\"" + Convert.ToDateTime(notice.NoticeDate).ToString("yyyy-MM-dd HH:mm") + "\",";
                json += "\"publisher\":\"" + notice.NoticePublisher.Name + "\",";
                json += "\"belong\":\"" + notice.NoticeBelong.DName + "\",";
                json += "\"hits\":\"" + notice.NoticeHits + "\"";

                json += "}";

                return Content(json);
            }
            else if (kind == "news_title")
            {
                News news = new News();
                news = new NewsManager().GetNewsById(id);
                new NewsManager().AddNewsHits(id);

                string json = "{\"kind\":\"news\",";

                json += "\"id\":\"" + news.id + "\",";
                json += "\"title\":\"" + news.NewsTitle + "\",";
                json += "\"main\":\"" + news.NewsMain + "\",";
                json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToString("yyyy-MM-dd HH:mm") + "\",";
                json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                json += "\"hits\":\"" + news.NewsHits + "\"";

                json += "}";

                return Content(json);
            }
            else
            {
                return JavaScript("alert('发生未知错误！！')");
            }
        }

        /*Home/UpdateStudentByUser
        1、获取用户要修改的信息
        2、提交信息
        3、返回操作结果*/
        public ActionResult UpdateStudentByUser(string number, string phone, string email)
        {
            if (new UserManager().UpdateStudentBasic(number, phone, email))
            {
                return Content("true");
            }
            return Content("false");
        }

        /*Home/ChangePassword1
        1、获取信息（学号、原密码、新密码）
        2、执行UserManager-changepassword1
        3、返回执行结果*/
        public ActionResult ChangePassword1(string number, string oldpassword, string newpassword)
        {
            if (new UserManager().ChangePasswordByOldPassword(number, oldpassword, newpassword))
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }

        /*Home/ChangePassword2
        1、获取信息（学号、邮箱、新密码）
        2、执行UserManager-changepassword2
        3、返回执行结果*/
        public ActionResult ChangePassword2(string number, string email, string newpassword)
        {
            if (new UserManager().ChangePasswordByEmail(number, email, newpassword))
            {
                return Content("success");
            }
            else
            {
                return Content("error");
            }
        }
    }
}