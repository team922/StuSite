using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class News
    {
        //新闻信息表

        public int id { get; set; }                    //新闻ID
        public string NewsTittle { get; set; }         //新闻标题
        public string NewsMain { get; set; }           //新闻主题
        public DateTime? NewsDate { get; set; }        //发布日期
        public DateTime? NewsDatetime { get; set; }    //发布日期+时间
        public Admin NewsPublisher { get; set; }       //发布者
        public NState NState { get; set; }             //新闻状态
    }
}
