using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class PergoDataVM
    {
        public IEnumerable<PergoData> list { get; set; }
        public string sumdistance { get; set; }
        public string avgspeed { get; set; }
    }
}