using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.Areas.AgroPark.ModelViews
{
    public class StockViewModel
    {
        public int STATUSID { get; set; }
        public int TYPE { get; set; }
        public int PRODUCT_ID { get; set; }
        public decimal QUANTITY { get; set; }
        public int PLACEID { get; set; }
    }
}