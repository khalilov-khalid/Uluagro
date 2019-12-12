using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class PlanDataVM
    {
        public IEnumerable<PLANNINGPLANDETAILSWORK> planlist { get; set; }
        public decimal area { get; set; }
        public decimal summary { get; set; }
    }
}