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
        public string NoticeTittle { get; set; }         //公告标题
        public string NoticeMain { get; set; }           //公告主题
        public DateTime? NoticeDate { get; set; }        //发布日期
        public DateTime? NoticeDatetime { get; set; }    //发布日期+时间
        public TBasic NoticePublisher { get; set; }      //发布者
        public Department NoticeBelong { get; set; }     //公告类别
        public NState NState { get; set; }               //公告状态
    }
}
