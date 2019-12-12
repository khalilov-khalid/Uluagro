using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class mainWorkPlan
    {
        public DateTime mainstartdate { get; set; }
        public DateTime mainenddate { get; set; }
        public int parcelId { get; set; }
        public int responderTd { get; set; }
        public int WorkId { get; set; }
        public List<dailyPlan> dailyWorkPlanArray { get; set; }
    }
}