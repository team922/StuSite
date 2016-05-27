using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StuSiteMVC.DAL;
using StuSiteMVC.Models;

namespace StuSiteMVC.BLL
{
    public class StateManager
    {
        public NState GetNStateById(int id)
        {
            return new NStateService().GetNStateById(id);
        }
    }
}
