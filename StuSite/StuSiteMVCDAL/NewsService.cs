using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using StuSiteMVC.Models;

namespace StuSiteMVC.DAL
{
    public class NewsService
    {
        //获取正常News
        public List<News> GetNews()
        {
            List<News> newslist = new List<News>();
            //sql语句
            string sql = "select * from News,NState where News.NState=NState.NStateId and NState.NStateId=1";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    News news = new News();
                    news.id = (int)row["id"];
                    news.NewsTitle = (string)row["NewsTitle"];
                    news.NewsMain = (string)row["NewsMain"];
                    news.NewsDate = row["NewsDate"] != DBNull.Value ? (DateTime?)row["NewsDate"] : null;
                    news.NewsPublisher = new AdminService().GetAdminById((int)row["NewsPublisher"]);
                    news.NState = new NStateService().GetNStateById((int)row["NState"]);
                    news.NewsHits = (int)row["NewsHits"];

                    newslist.Add(news);
                }
            }
            return newslist;
        }

        //存储过程分页显示News
        public List<News> GetNewsByPage(int pagesize, int pageindex, ref int pagecount, int datacount)
        {
            List<News> newslist = new List<News>();

            SqlParameter para1 = new SqlParameter("@pagecount", SqlDbType.Int);
            para1.Direction = ParameterDirection.Output;
            SqlParameter para2 = new SqlParameter("@datacount", SqlDbType.Int);
            para2.Direction = ParameterDirection.Output;

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@pageindex",pageindex),
                para1,
                para2
            };

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.StoredProcedure, "SP_PageNews", paras);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    News news = new News();
                    news.id = (int)row["id"];
                    news.NewsTitle = (string)row["NewsTitle"];
                    news.NewsMain = (string)row["NewsMain"];
                    news.NewsDate = row["NewsDate"] != DBNull.Value ? (DateTime?)row["NewsDate"] : null;
                    news.NewsPublisher = new AdminService().GetAdminById((int)row["NewsPublisher"]);
                    news.NState = new NStateService().GetNStateById((int)row["NState"]);
                    news.NewsHits = (int)row["NewsHits"];

                    newslist.Add(news);
                }
                pagecount = Convert.ToInt32(para1.Value);
                datacount = Convert.ToInt32(para2.Value);
            }
            return newslist;
        }

        //获取置顶News
        public News GetTopNews()
        {
            //sql语句
            string sql = "select * from News,NState where News.NState=NState.NStateId and NState.NStateId=2";
            News topnews = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    AdminService adminService = new AdminService();
                    NStateService nstateService = new NStateService();

                    topnews = new News();
                    topnews.id = (int)reader["id"];
                    topnews.NewsTitle = (string)reader["NewsTitle"];
                    topnews.NewsMain = (string)reader["NewsMain"];
                    topnews.NewsDate = reader["NewsDate"] != DBNull.Value ? (DateTime?)reader["NewsDate"] : null;
                    topnews.NewsPublisher = adminService.GetAdminById((int)reader["NewsPublisher"]);
                    topnews.NState = nstateService.GetNStateById((int)reader["NState"]);
                    topnews.NewsHits = (int)reader["NewsHits"];
                }
            }
            return topnews;
        }

        //获取最新10条新闻
        public List<News> GetTop10News()
        {
            List<News> newslist = new List<News>();
            //sql语句
            string sql = "select top 10 * from News,NState where News.NState=NState.NStateId and NState.NStateId=1 order by NewsDate desc";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    News news = new News();
                    news.id = (int)row["id"];
                    news.NewsTitle = (string)row["NewsTitle"];
                    news.NewsMain = (string)row["NewsMain"];
                    news.NewsDate = row["NewsDate"] != DBNull.Value ? (DateTime?)row["NewsDate"] : null;
                    news.NewsPublisher = new AdminService().GetAdminById((int)row["NewsPublisher"]);
                    news.NState = new NStateService().GetNStateById((int)row["NState"]);
                    news.NewsHits = (int)row["NewsHits"];

                    newslist.Add(news);
                }
            }
            return newslist;
        }

        //获取Newsbyid
        public News GetNewsById(int id)
        {
            //sql语句
            string sql = "select * from News where ID=@id";
            News news = new News();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)))
            {
                if (reader.Read())
                {
                    AdminService adminService = new AdminService();
                    NStateService nstateService = new NStateService();

                    news = new News();
                    news.id = (int)reader["id"];
                    news.NewsTitle = (string)reader["NewsTitle"];
                    news.NewsMain = (string)reader["NewsMain"];
                    news.NewsDate = reader["NewsDate"] != DBNull.Value ? (DateTime?)reader["NewsDate"] : null;
                    news.NewsPublisher = adminService.GetAdminById((int)reader["NewsPublisher"]);
                    news.NState = nstateService.GetNStateById((int)reader["NState"]);
                    news.NewsHits = (int)reader["NewsHits"];
                }
            }
            return news;
        }

        //添加点击次数（浏览量）
        public bool AddNewsHits(int id)
        {
            string sql = "update News set NewsHits=(NewsHits+1) where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }

        //添加News
        public bool AddNews(News news)
        {
            //1.sql语句
            string sql = "insert into News(NewsTitle,NewsMain,NewsDate,NewsPublisher,NState,NewsHits)"
                         + " values(@NewsTitle,@NewsMain,@NewsDate,@NewsPublisher,@NState,@NewsHits)";
            sql += " select @@identity";
            //2.参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@NewsTitle",news.NewsTitle),
                new SqlParameter("@NewsMain",news.NewsMain),
                new SqlParameter("@NewsDate",news.NewsDate),
                new SqlParameter("@NewsPublisher",news.NewsPublisher.id),
                new SqlParameter("@NState",news.NState.NStateId),
                new SqlParameter("@NewsHits",news.NewsHits),
              };
            //3、执行sql语句
            news.id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para));
            if (news.id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //设置置顶
        public bool SetNewsTopic(int id)
        {
            string sql = "update News set Nstate=2 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }

        //取消置顶
        public bool RemoveNewsTopic()
        {
            string sql = "update News set Nstate=1";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql) > 0;
        }

        //删除News（逻辑删除）
        public bool DeleteNewsById(int id)
        {
            string sql = "update News set Nstate=0 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }
    }
}
