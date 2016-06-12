﻿using System;
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

        public ActionResult DeleteNoticeAndNews(string type,int id)
        {
            if (type=="notice")
            {
                if (new NoticeManager().DeleteNoticeById(id))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            else
            {
                if (new NewsManager().DeleteNewsById(id))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
        }

        public ActionResult ChangeTopic(string type,string activity, int id)
        {
            if (type == "notice")
            {
                if (activity == "settopic")
                {
                    if (new NoticeManager().SetNoticeTopic(id))
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content("false");
                    }
                }
                else
                {
                    if (new NoticeManager().RemoveNoticeTopic())
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content("false");
                    }
                }
            }
            else
            {
                if (activity == "settopic")
                {
                    if (new NewsManager().SetNewsTopic(id))
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content("false");
                    }
                }
                else
                {
                    if (new NewsManager().RemoveNewsTopic())
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

        public ActionResult ImageUpload(HttpPostedFileBase upload)
        {
            string name = System.IO.Path.GetFileName(upload.FileName);
            string suffix = "";
            if (name.EndsWith(".jpg") || name.EndsWith(".JPG") || name.EndsWith(".jpeg") || name.EndsWith(".JPEG") || name.EndsWith(".jpe") || name.EndsWith(".JPE"))
            {
                suffix = ".jpg";
            }
            else if (name.EndsWith(".png") || name.EndsWith(".PNG") || name.EndsWith(".pns") || name.EndsWith(".PNS"))
            {
                suffix = ".png";
            }
            else if (name.EndsWith(".gif") || name.EndsWith(".GIF"))
            {
                suffix = ".gif";
            }
            if (suffix == "")
            {
                return Content("<script>window.parent.CKEDITOR.tools.callFunction('文件格式不正确（必须为.jpg/.gif/.bmp/.png文件）');</script>");
            }
            else
            {
                Guid guid = Guid.NewGuid();
                string filename = guid + suffix;
                string filePhysicalPath = Server.MapPath("~/Images/Info/" + filename);//将图片保存在~/Images/Info文件夹
                Guid g = Guid.NewGuid();
                upload.SaveAs(filePhysicalPath);

                string url = "/Images/Info/" + filename;
                string CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

                //上传成功后，把图片返回到第一个tab选项
                return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
            }
        }
    }
}