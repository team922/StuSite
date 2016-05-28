﻿using System;
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

        public ActionResult SetTopNews()
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
                json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToShortDateString().ToString() + "\",";
                json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                json += "\"hits\":\"" + news.NewsHits + "\"";
                
                json += "}";
                return Content(json);
            }
        }

        public ActionResult SetNews()
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
                    json += "\"date\":\"" + Convert.ToDateTime(news.NewsDate).ToShortDateString().ToString() + "\",";
                    json += "\"publisher\":\"" + news.NewsPublisher.Name + "\",";
                    json += "\"hits\":\"" + news.NewsHits + "\"";
                    json += "},";
                }
                json = json.Substring(0, json.Length - 1);
                json += "],\"number\":\""+i+"\"}";
                return Content(json);
            }
        }
    }
}