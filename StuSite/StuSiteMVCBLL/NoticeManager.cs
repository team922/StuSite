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
        //获取全部分类
        public List<Department> GetAllDepartment()
        {
            return new DepartmentService().GetDepartment();
        }

        //获取分类byid
        public Department GetDepartment(int id)
        {
            return new DepartmentService().GetDepartmentById(id);
        }

        //添加公告
        public bool AddNotice(Notices notice)
        {
            if (notice.NState.NStateId == 2)
            {
                if (new NoticesService().RemoveNoticeTopic())
                {
                    return new NoticesService().AddNotices(notice);
                }
                else
                {
                    return false;
                }
            }
            return new NoticesService().AddNotices(notice);
        }

        //获取top公告
        public Notices GetTopNotice()
        {
            return new NoticesService().GetTopNotices();
        }

        //获取top10公告
        public List<Notices> GetTop10Notice()
        {
            return new NoticesService().GetTop10Notices();
        }

        //获取公告by分类
        public List<Notices> GetSelectNotice(int department)
        {
            return new NoticesService().GetSelectNotice(department);
        }

        //获取公告byid
        public Notices GetNoticeById(int id)
        {
            return new NoticesService().GetNoticeById(id);
        }

        //获取公告（存储过程）
        public List<Notices> GetNoticeByPage(int pagesize, int pageindex, ref int pagecount, int datacount)
        {
            return new NoticesService().GetNoticeByPage(pagesize, pageindex, ref pagecount, datacount);
        }

        //添加点击数
        public bool AddNoticeHits(int id)
        {
            return new NoticesService().AddNoticeHits(id);
        }

        //设置置顶by id
        public bool SetNoticeTopic(int id)
        {
            if (RemoveNoticeTopic())
            {
                return new NoticesService().SetNoticeTopic(id);
            }
            else
            {
                return false;
            }
        }

        //取消置顶
        public bool RemoveNoticeTopic()
        {
            return new NoticesService().RemoveNoticeTopic();
        }

        //删除公告by id（逻辑删除set nstate=0）
        public bool DeleteNoticeById(int id)
        {
            return new NoticesService().DeleteNoticeById(id);
        }
    }
}
