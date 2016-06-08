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

        public ActionResult ShowDetail()
        {
            return View();
        }

        public ActionResult ShowList()
        {
            return View();
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

        public ActionResult GetIPMessage()
        {
            return Content(new IPManager().GetAddress());
        }

        public ActionResult GetAreaid(string district)
        {
            return Content(new IPManager().GetAreaid(district));
        }

        public ActionResult GetWeather(string id)
        {
            return Content(new IPManager().GetWeather(id));
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

        public ActionResult GetSelectNotice(string department)
        {
            int id = Convert.ToInt16(department);
            List<Notices> listnotice = new List<Notices>();
            if (id==0)
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
                if (i==0)
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

        public ActionResult ShowSelectMessage(string kind, int id)
        {
            if (kind=="notice_title")
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
            else if (kind=="news_title")
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
    }
}