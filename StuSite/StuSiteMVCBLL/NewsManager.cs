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
            if (news.NState.NStateId==2)
            {
                if (new NewsService().RemoveNewsTopic())
                {
                    return new NewsService().AddNews(news);
                }
                else
                {
                    return false;
                }
            }
            return new NewsService().AddNews(news);
        }

        //获取置顶新闻
        public News GetTopNews()
        {
            return new NewsService().GetTopNews();
        }

        //获取最新10条新闻
        public List<News> GetTop10News()
        {
            return new NewsService().GetTop10News();
        }

        //获取新闻byid
        public News GetNewsById(int id)
        {
            return new NewsService().GetNewsById(id);
        }

        //获取新闻（存储过程）
        public List<News> GetNewsByPage(int pagesize, int pageindex, ref int pagecount, int datacount)
        {
            return new NewsService().GetNewsByPage(pagesize, pageindex, ref pagecount, datacount);
        }

        //添加新闻点击数
        public bool AddNewsHits(int id)
        {
            return new NewsService().AddNewsHits(id);
        }

        //设置置顶by id
        public bool SetNewsTopic(int id)
        {
            if (RemoveNewsTopic())
            {
                return new NewsService().SetNewsTopic(id);
            }
            else
            {
                return false;
            }
        }

        //取消置顶
        public bool RemoveNewsTopic()
        {
            return new NewsService().RemoveNewsTopic();
        }

        //删除新闻by id（逻辑删除set nstate=0）
        public bool DeleteNewsById(int id)
        {
            return new NewsService().DeleteNewsById(id);
        }
    }
}
