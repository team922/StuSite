using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuSiteMVC.Models
{
    public class Major
    {
        //专业表
        public string MajorId { get; set; }           //专业ID
        public string MajorName { get; set; }      //专业名称
        public College Belong { get; set; }        //所属学院
    }
}
