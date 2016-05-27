using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuSiteMVC.DAL;
using StuSiteMVC.Models;

namespace StuSiteMVC.BLL
{
    public class NewsManager
    {
        //添加新闻
        public bool AddNews(News news)
        {
            return new NewsService().AddNews(news);
        }

        //获取置顶新闻
        public News GetTopNews()
        {
            return new NewsService().GetTopNews();
        }
    }
}
