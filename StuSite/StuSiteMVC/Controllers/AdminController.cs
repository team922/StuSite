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

        //学生信息界面
        public ActionResult StuManage1()
        {
            return View();
        }

        //学生注册界面
        public ActionResult StuManage2()
        {
            return View();
        }

        //学生状态管理界面
        public ActionResult StuManage3()
        {
            return View();
        }

        //公告发布界面
        public ActionResult NoticeManage1()
        {
            return View();
        }

        //公告管理界面
        public ActionResult NoticeManage2()
        {
            return View();
        }

        //新闻发布界面
        public ActionResult NewsManage1()
        {
            return View();
        }

        //新闻管理界面
        public ActionResult NewsManage2()
        {
            return View();
        }

        //ajax
        /*Admin/GetStudent
        1、获取学生信息（参数为学号）
        2、通过业务逻辑层UserManager-GetStudent获取学生信息
        3、根据返回信息shengcJson字符串
        4、返回Json*/
        public ActionResult GetStudent(string id)
        {
            SBasic sbasic = new SBasic();
            try
            {
                sbasic = new UserManager().GetStudent(id);
                string json = "{";

                json += "\"number\":\"" + sbasic.SNumber + "\",";
                json += "\"name\":\"" + sbasic.SName + "\",";
                json += "\"idnumber\":\"" + sbasic.SIDNumber + "\",";
                json += "\"college\":\"" + sbasic.SCollege.CollegeName + "\",";
                json += "\"major\":\"" + sbasic.SMajor.MajorName + "\",";
                json += "\"enrollment\":\"" + Convert.ToDateTime(sbasic.SEnrollment).ToString("yyyy-MM-dd") + "\",";
                json += "\"sex\":\"" + sbasic.SSex.Trim() + "\",";
                json += "\"phone\":\"" + sbasic.SPhone + "\",";
                json += "\"email\":\"" + sbasic.SEmail + "\",";
                json += "\"birthday\":\"" + Convert.ToDateTime(sbasic.SBirthday).ToString("yyyy-MM-dd") + "\",";
                json += "\"address\":\"" + sbasic.SAddress + "\"";

                json += "}";
                return Content(json);
            }
            catch
            {
                return Content("false");
            }
        }

        /*Admin/UpdateStudentByAdmin
        1、获取要修改的学生信息
        2、通过业务逻辑层UserManager-UpdateStudentBasic修改学生信息
        3、返回执行结果（json）*/
        public ActionResult UpdateStudentByAdmin(string number, string name, string idnumber, string sex, string birthday, string address)
        {
            if (new UserManager().UpdateStudentBasic(number, name, idnumber,sex,birthday,address))
            {
                return Content("true");
            }
            return Content("false");
        }

        /*Admin/GetCollege
        1、通过业务逻辑层UserManager-GetCollege
        2、获取院校信息
        3、编写Json字符串
        4、返回信息（Json）*/
        public ActionResult GetCollege()
        {
            List<College> collegelist = new List<College>();
            collegelist = new UserManager().GetCollege();

            int i = 0;
            string json = "{\"collegelist\":[";
            foreach (var college in collegelist)
            {
                i++;
                json += "{";
                json += "\"id\":\"" + college.CollegeId.Trim() + "\",";
                json += "\"name\":\"" + college.CollegeName.Trim() + "\"";
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "],\"number\":\"" + i + "\"}";
            return Content(json);
        }

        /*Admin/GetSelectMajor
        1、通过url接收院校id
        2、通过UserManager-GetSelectMajor获取该院校专业列表（参数为学院id-string类型）
        3、接受返回的专业信息
        4、编写Json字符串并返回*/
        public ActionResult GetSelectMajor(string collegeid)
        {
            List<Major> majorlist = new List<Major>();
            majorlist = new UserManager().GetMajorByCollegeid(collegeid);

            int i = 0;
            string json = "{\"majorlist\":[";
            foreach (var major in majorlist)
            {
                i++;
                json += "{";
                json += "\"id\":\"" + major.MajorId.Trim() + "\",";
                json += "\"name\":\"" + major.MajorName.Trim() + "\"";
                json += "},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "],\"number\":\"" + i + "\"}";
            return Content(json);
        }

        /*Admin/GetIDCardMessage
        1、获取要查询的身份证号（URL参数idcard）
        2、通过IPManager-GetIDCardMessages获取该身份证号信息（api）
        3、返回Json字符串*/
        public ActionResult GetIDCardMessage(string idcard)
        {
            return Content(new IPManager().GetIDCardMesage(idcard));
        }

        /*Admin/AddStudent
        1、获取学生信息（url参数）
        2、利用UserManager-AddStudent添加学生并返回学号、
        2、返回添加结果*/
        public ActionResult AddStudent(string name, string phone, string idcard, string sex, DateTime birthday, string address, string college_id, string major_id)
        {

            DateTime datetime = new IPManager().GetDateTime();
            string year = datetime.ToString("yyyy");
            string date = datetime.ToString("yyyy-MM-dd");
            string latestnumber = new UserManager().SelectTheLatestData(major_id, year);
            string newnumber = "";
            if (latestnumber=="0")
            {
                newnumber = year + major_id + "001";
            }
            else
            {
                newnumber = Convert.ToString(Convert.ToInt32(latestnumber) + 1);
            }

            string password = phone.Substring(3, 8);

            SLogin slogin = new SLogin();
            SBasic sbasic = new SBasic();

            College college = new College();
            college.CollegeId = college_id;
            Major major = new Major();
            major.MajorId = major_id;
            Status status = new Status();
            status.StatusId = 0;
            State state = new State();
            state.StateId = 1;

            sbasic.SNumber = newnumber;
            sbasic.SName = name;
            sbasic.SIDNumber = idcard;
            sbasic.SCollege = college;
            sbasic.SMajor = major;
            sbasic.SEnrollment = Convert.ToDateTime(date);
            sbasic.SStatus = status;
            sbasic.SSex = sex;
            sbasic.SPhone = phone;
            sbasic.SBirthday = birthday;
            sbasic.SAddress = address;
            sbasic.SPicAddress = "default";

            slogin.SNumber = sbasic;
            slogin.SPassword = password;
            slogin.State = state;

            if(new UserManager().AddStudent(slogin)== newnumber)
            {
                return Content(newnumber);
            }
            else
            {
                return Content("0");
            }
        }

        /*Admin/GetLocked
        1、获取冻结的用户
        2、返回冻结用户信息*/
        public ActionResult GetLocked()
        {
            List<SLogin> sloginlist = new List<SLogin>();
            sloginlist = new UserManager().GetLocked();
            if (sloginlist == null)
            {
                return Content("false");
            }
            else
            {
                int i = 0;
                string json = "{\"sloginlist\":[";
                foreach (var slogin in sloginlist)
                {
                    i++;
                    json += "{";
                    json += "\"id\":\"" + slogin.SNumber.SNumber + "\",";
                    json += "\"name\":\"" + slogin.SPassword + "\"";
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

        /*Admin.LockByNumber
        1、获取要冻结的用户学号
        2、通过UserManager-LockByNumber冻结指定用户
        3、返回操作结果*/
        public ActionResult LockByNumber(string number)
        {
            if (new UserManager().LockStudent(number))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /*Admin/LockByMonth
        1、获取按月份冻结用户的参数（月份 int or string）
        2、通过UserManager-LockByMonth冻结指定条件的用户
        3、返回操作结果*/
        public ActionResult LockByMonth(string month)
        {
            DateTime datetime = new IPManager().GetDateTime();
            datetime = datetime.AddMonths(-Convert.ToInt32(month));

            string date = datetime.ToString("yyy-MM-dd");
            if (new UserManager().LockStudentByMonth(date))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /*Admin/UnlockByNumber
        1、获取要操作的用户学号
        2、调用UserManager-UnlockByNumber
        3、返回操作结果*/
        public ActionResult UnlockByNumber(string number)
        {
            if (new UserManager().UnlockStudent(number))
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

        /*Admin/AddNews
        1、通过request获取新闻信息
        2、通过NewsManager-AddNews添加新闻信息
        3、返回添加结果*/
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

        /*Admin/AddNotice
        1、通过request获取公告信息
        2、通过NewsManager-AddNotice添加公告信息
        3、返回添加结果*/
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

        /*Admin/DeleteNoticeAndNews
        1、获取操作类型与数据id（url参数）
        2、执行操作（type=notice  or  type=news）
        3、返回操作结果*/
        public ActionResult DeleteNoticeAndNews(string type, int id)
        {
            if (type == "notice")
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

        /*Admin/ChangeTopic
        1、获取要修改状态的类型、操作、id
        2、执行不同的方法完成操作
        3、返回结果*/
        public ActionResult ChangeTopic(string type, string activity, int id)
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

        /*Admin/ImageUpload
        1、ckediter 图片上传方法*/
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