using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuSiteMVC.DAL;
using StuSiteMVC.Models;

namespace StuSiteMVC.BLL
{
    public class NoticeManager
    {
        //获取top公告
        public void GetTopNotice(out Notices topnotice)
        {
            topnotice = new NoticesService().GetTopNotices();
        }

        //获取top10公告
        public void GetTop10Notice(out List<Notices> top10notice)
        {
            top10notice = new NoticesService().GetTop10Notices();
        }
    }
}
