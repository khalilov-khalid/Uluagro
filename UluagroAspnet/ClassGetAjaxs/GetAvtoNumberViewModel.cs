using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.ClassGetAjaxs
{
    public class GetAvtoNumberViewModel
    {
        public IEnumerable<GetAvtoNumber> list { get; set; }
        public decimal pagecount { get; set; }
        public int currentpage { get; set; }
    }
}