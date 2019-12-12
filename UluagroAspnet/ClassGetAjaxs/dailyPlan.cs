using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class dailyPlan
    {
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int queueId { get; set; }
        public List<queuePlan> queueArray { get; set; }
    }
}