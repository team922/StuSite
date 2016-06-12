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
    public class NoticesService
    {
        //获取正常Notices
        public List<Notices> GetNotices()
        {
            List<Notices> noticeslist = new List<Notices>();
            //sql语句
            string sql = "select * from Notices,NState where Notices.NState=NState.NStateId and NState.NStateId=1";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Notices notices = new Notices();
                    notices.id = (int)row["id"];
                    notices.NoticeTitle = (string)row["NoticeTitle"];
                    notices.NoticeMain = (string)row["NoticeMain"];
                    notices.NoticeDate = row["NoticeDate"] != DBNull.Value ? (DateTime?)row["NoticeDate"] : null;
                    notices.NoticePublisher = new AdminService().GetAdminById((int)row["NoticePublisher"]);
                    notices.NoticeBelong = new DepartmentService().GetDepartmentById((int)row["NoticeBelong"]);
                    notices.NState = new NStateService().GetNStateById((int)row["NState"]);
                    notices.NoticeHits = (int)row["NoticeHits"];

                    noticeslist.Add(notices);
                }
            }
            return noticeslist;
        }

        //存储过程分页显示Notices
        public List<Notices> GetNoticeByPage(int pagesize, int pageindex, ref int pagecount,int datacount)
        {
            List<Notices> noticeslist = new List<Notices>();

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

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.StoredProcedure, "SP_PageNotice", paras);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Notices notices = new Notices();
                    notices.id = (int)row["id"];
                    notices.NoticeTitle = (string)row["NoticeTitle"];
                    notices.NoticeMain = (string)row["NoticeMain"];
                    notices.NoticeDate = row["NoticeDate"] != DBNull.Value ? (DateTime?)row["NoticeDate"] : null;
                    notices.NoticePublisher = new AdminService().GetAdminById((int)row["NoticePublisher"]);
                    notices.NoticeBelong = new DepartmentService().GetDepartmentById((int)row["NoticeBelong"]);
                    notices.NState = new NStateService().GetNStateById((int)row["NState"]);
                    notices.NoticeHits = (int)row["NoticeHits"];

                    noticeslist.Add(notices);
                }
                pagecount = Convert.ToInt32(para1.Value);
                datacount = Convert.ToInt32(para2.Value);
            }
            return noticeslist;
        }

        //获取置顶Notices
        public Notices GetTopNotices()
        {
            //sql语句
            string sql = "select * from Notices,NState where Notices.NState=NState.NStateId and NState.NStateId=2";
            Notices topnotices = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    AdminService adminService = new AdminService();
                    DepartmentService departmentService = new DepartmentService();
                    NStateService nstateService = new NStateService();

                    topnotices = new Notices();
                    topnotices.id = (int)reader["id"];
                    topnotices.NoticeTitle = (string)reader["NoticeTitle"];
                    topnotices.NoticeMain = (string)reader["NoticeMain"];
                    topnotices.NoticeDate = reader["NoticeDate"] != DBNull.Value ? (DateTime?)reader["NoticeDate"] : null;
                    topnotices.NoticePublisher = adminService.GetAdminById((int)reader["NoticePublisher"]);
                    topnotices.NoticeBelong = departmentService.GetDepartmentById((int)reader["NoticeBelong"]);
                    topnotices.NState = nstateService.GetNStateById((int)reader["NState"]);
                    topnotices.NoticeHits = (int)reader["NoticeHits"];
                }
            }
            return topnotices;
        }

        //获取前10条Notices
        public List<Notices> GetTop10Notices()
        {
            List<Notices> noticeslist = new List<Notices>();
            //sql语句
            string sql = "select top 10 * from Notices,NState where Notices.NState=NState.NStateId and NState.NStateId=1 order by NoticeDate desc";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Notices notices = new Notices();
                    notices.id = (int)row["id"];
                    notices.NoticeTitle = (string)row["NoticeTitle"];
                    notices.NoticeMain = (string)row["NoticeMain"];
                    notices.NoticeDate = row["NoticeDate"] != DBNull.Value ? (DateTime?)row["NoticeDate"] : null;
                    notices.NoticePublisher = new AdminService().GetAdminById((int)row["NoticePublisher"]);
                    notices.NoticeBelong = new DepartmentService().GetDepartmentById((int)row["NoticeBelong"]);
                    notices.NState = new NStateService().GetNStateById((int)row["NState"]);

                    noticeslist.Add(notices);
                }
            }
            return noticeslist;
        }

        //获取公告by分类
        public List<Notices> GetSelectNotice(int department)
        {
            List<Notices> noticeslist = new List<Notices>();
            //sql语句
            string sql = "select top 10 * from Notices,NState,Department where Notices.NState=NState.NStateId and NState.NStateId=1 and Notices.Noticebelong=Department.Did and Department.Did=@department order by NoticeDate desc";
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.Text, sql,new SqlParameter("@department", department));
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Notices notices = new Notices();
                    notices.id = (int)row["id"];
                    notices.NoticeTitle = (string)row["NoticeTitle"];
                    notices.NoticeMain = (string)row["NoticeMain"];
                    notices.NoticeDate = row["NoticeDate"] != DBNull.Value ? (DateTime?)row["NoticeDate"] : null;
                    notices.NoticePublisher = new AdminService().GetAdminById((int)row["NoticePublisher"]);
                    notices.NoticeBelong = new DepartmentService().GetDepartmentById((int)row["NoticeBelong"]);
                    notices.NState = new NStateService().GetNStateById((int)row["NState"]);

                    noticeslist.Add(notices);
                }
            }
            return noticeslist;
        }
        
        //获取公告byid
        public Notices GetNoticeById(int id)
        {
            Notices notices = new Notices();
            //sql语句
            string sql = "select * from Notices where ID=@id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)))
            {
                if (reader.Read())
                {
                    AdminService adminService = new AdminService();
                    DepartmentService departmentService = new DepartmentService();
                    NStateService nstateService = new NStateService();

                    notices = new Notices();
                    notices.id = (int)reader["id"];
                    notices.NoticeTitle = (string)reader["NoticeTitle"];
                    notices.NoticeMain = (string)reader["NoticeMain"];
                    notices.NoticeDate = reader["NoticeDate"] != DBNull.Value ? (DateTime?)reader["NoticeDate"] : null;
                    notices.NoticePublisher = adminService.GetAdminById((int)reader["NoticePublisher"]);
                    notices.NoticeBelong = departmentService.GetDepartmentById((int)reader["NoticeBelong"]);
                    notices.NState = nstateService.GetNStateById((int)reader["NState"]);
                    notices.NoticeHits = (int)reader["NoticeHits"];
                }
            }
            return notices;
        }

        //添加点击次数（浏览量）
        public bool AddNoticeHits(int id)
        {
            string sql = "update Notices set NoticeHits=(NoticeHits+1) where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }

        //添加Notices
        public bool AddNotices(Notices notices)
        {
            //1.sql语句
            string sql = "insert into Notices(NoticeTitle,NoticeMain,NoticeDate,NoticePublisher,NoticeBelong,NState,NoticeHits)"
                         + " values(@NoticeTitle,@NoticeMain,@NoticeDate,@NoticePublisher,@NoticeBelong,@NState,@NoticeHits)";
            sql += " select @@identity";
            //2.参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@NoticeTitle",notices.NoticeTitle),
                new SqlParameter("@NoticeMain",notices.NoticeMain),
                new SqlParameter("@NoticeDate",notices.NoticeDate),
                new SqlParameter("@NoticePublisher",notices.NoticePublisher.id),
                new SqlParameter("@NoticeBelong",notices.NoticeBelong.Did),
                new SqlParameter("@NState",notices.NState.NStateId),
                new SqlParameter("@NoticeHits",notices.NoticeHits),
              };
            //3、执行sql语句
            notices.id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para));
            if (notices.id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //设置置顶
        public bool SetNoticeTopic(int id)
        {
            string sql = "update Notices set Nstate=2 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }

        //取消置顶
        public bool RemoveNoticeTopic()
        {
            string sql = "update Notices set Nstate=1";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql) > 0;
        }

        //删除News（逻辑删除）
        public bool DeleteNoticeById(int id)
        {
            string sql = "update Notices set Nstate=0 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", id)) > 0;
        }
    }
}
