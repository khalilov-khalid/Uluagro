using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class CopyPayment
    {
        public int OBJECTID { get; set; }
        public DateTime DATE { get; set; }
        public string ID_INOUT { get; set; }
        public string PAYMENT_TYPE_ID { get; set; }
        public decimal MAIN_PAYMENT { get; set; }
        public decimal VAT { get; set; }
        public decimal ALL_PAYMENT { get; set; }
        public string RELEASE { get; set; }
        public decimal REMNANT { get; set; }
        public string NOTE { get; set; }
    }
}