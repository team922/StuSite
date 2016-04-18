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
                    notices.NoticeTittle = (string)row["NoticeTittle"];
                    notices.NoticeMain = (string)row["NoticeMain"];
                    notices.NoticeDate = row["NoticeDate"] != DBNull.Value ? (DateTime?)row["NoticeDate"] : null;
                    notices.NoticeDatetime = row["NoticeDatetime"] != DBNull.Value ? (DateTime?)row["NoticeDatetime"] : null;
                    notices.NoticePublisher = new TBasicService().GetTeacherBsaicByTNumber((string)row["NoticePublisher"]);
                    notices.NoticeBelong = new DepartmentService().GetDepartmentById((int)row["NoticeBelong"]);
                    notices.NState = new NStateService().GetNStateById((int)row["NState"]);

                    noticeslist.Add(notices);
                }
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
                    TBasicService tBasicService = new TBasicService();
                    DepartmentService departmentService = new DepartmentService();
                    NStateService nstateService = new NStateService();

                    topnotices = new Notices();
                    topnotices.id = (int)reader["id"];
                    topnotices.NoticeTittle = (string)reader["NoticeTittle"];
                    topnotices.NoticeMain = (string)reader["NoticeMain"];
                    topnotices.NoticeDate = reader["NoticeDate"] != DBNull.Value ? (DateTime?)reader["NoticeDate"] : null;
                    topnotices.NoticeDatetime = reader["NoticeDatetime"] != DBNull.Value ? (DateTime?)reader["NoticeDatetime"] : null;
                    topnotices.NoticePublisher = tBasicService.GetTeacherBsaicByTNumber((string)reader["NoticePublisher"]);
                    topnotices.NoticeBelong = departmentService.GetDepartmentById((int)reader["NoticeBelong"]);
                    topnotices.NState = nstateService.GetNStateById((int)reader["NState"]);
                }
            }
            return topnotices;
        }

        //添加Notices
        public void AddNotices(Notices notices)
        {
            //1.sql语句
            string sql = "insert into Notices(NoticeTittle,NoticeMain,NoticeDate,NoticeDatetime,NoticePublisher,NoticeBelong,NState)"
                         + " values(@NoticeTittle,@NoticeMain,@NoticeDate,@NoticeDatetime,@NoticePublisher,@NoticeBelong,@NState)";
            sql += " select @@identity";
            //2.参数赋值
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@NoticeTittle",notices.NoticeTittle),
                new SqlParameter("@NoticeMain",notices.NoticeMain),
                new SqlParameter("@NoticeDate",notices.NoticeDate),
                new SqlParameter("@NoticeDatetime",notices.NoticeDatetime),
                new SqlParameter("@NoticePublisher",notices.NoticePublisher.TNumber),
                new SqlParameter("@NoticeBelong",notices.NoticeBelong.Did),
                new SqlParameter("@NState",notices.NState.NStateId),
              };
            //3、执行sql语句
            notices.id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para));
        }

        //设置置顶
        public bool SetTopNotices(Notices notices)
        {
            string sql = "update Notices set Nstate=2 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", notices.id)) > 0;
        }

        //取消置顶
        public bool RemoveTopNotices(Notices notices)
        {
            string sql = "update Notices set Nstate=1 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", notices.id)) > 0;
        }

        //删除News（逻辑删除）
        public bool DeleteNotices(Notices notices)
        {
            string sql = "update Notices set Nstate=0 where id=@id";
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnString, CommandType.Text, sql, new SqlParameter("@id", notices.id)) > 0;
        }
    }
}
