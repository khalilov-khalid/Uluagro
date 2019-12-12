using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.ClassGetAjaxs;
using UluagroAspnet.Models;

namespace UluagroAspnet.Mainclasses
{
    public class MainViewModel
    {
        public List<Worker> _workers { get; set; }
        public List<FERTILIZER_STOCK> _ehtiyyat { get; set; }
        public User _loginedUser { get; set; }
        public int docmsgcount { get; set; }
        public List<WORK_PLAN> _workPlanList {get; set;}
        public CURRENT_ACCOUNT _CurrentAccount { get; set; }
        public List<PVOT_S> _Pvots { get; set; }
    }
}