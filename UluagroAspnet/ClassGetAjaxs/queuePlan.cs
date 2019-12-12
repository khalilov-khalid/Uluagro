using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class queuePlan
    {
        public string queuestartdate { get; set; }
        public string queueenddate { get; set; }
        public List<int> workerID { get; set; }
        public string note { get; set; }
        public string waterquantity { get; set; }
        public List<technicallist> technialArray { get; set; }
        public List<feltilizerlist> feltilizerArray { get; set; }
    }
}