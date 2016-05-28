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
                if (new NoticesService().RemoveTopNotices())
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
