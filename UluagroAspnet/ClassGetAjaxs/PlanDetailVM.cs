using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class PlanDetailVM
    {
        public PLANNINGPLAN selectedPlan { get; set; }
        public IEnumerable<PLANINGPLANBYDETAIL> detailsList { get; set; }
    }
}