using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class GetAvtoNumber
    {
        public string plaka { get; set; }
        public DateTime? tarih { get; set; }
        public int? kam_no { get; set; }
        public object durum { get; set; }
        public int count { get; set; }
    }
}