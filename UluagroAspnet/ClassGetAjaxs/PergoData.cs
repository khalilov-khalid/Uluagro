using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class PergoData
    {
        public object name { get; set; }
        public decimal distance { get; set; }
        public Int16 speed { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object direction { get; set; }
        public object terminal { get; set; }
        public DateTime logdate { get; set; }
    }
}