using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class Notices
    {
        //公告信息表
        public int id { get; set; }                      //公告ID
        public string NoticeTitle { get; set; }          //公告标题
        public string NoticeMain { get; set; }           //公告主题
        public DateTime? NoticeDate { get; set; }        //发布日期
        public Admin NoticePublisher { get; set; }       //发布者
        public Department NoticeBelong { get; set; }     //公告类别
        public NState NState { get; set; }               //公告状态
        public int NoticeHits { get; set; }              //浏览次数
    }
}
