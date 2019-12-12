using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;
using UluagroAspnet.ClassGetAjaxs;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class GarageDataVM
    {
        public IEnumerable<GarageData> list { get; set; }
        public decimal pagecount { get; set; }
        public int currentpage { get; set; }
    }
}