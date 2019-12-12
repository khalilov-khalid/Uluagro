using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.Areas.AgroPark.ModelViews
{
    public class StockTotalView
    {
        public int OBJECTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string CATEGORY { get; set; }
        public decimal QUANTITY { get; set; }
    }
}