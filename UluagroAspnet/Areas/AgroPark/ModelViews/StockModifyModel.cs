using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UluagroAspnet.Areas.AgroPark.ModelViews
{
    public class StockModifyModel
    {
        public int OBJECTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string DESTINATIONSTATUS { get; set; }
        public string MODIFYDATE { get; set; }
        public string QUANTITY { get; set; }
        public string PLACENAME { get; set; }
        public string WORKERNAME { get; set; }

    }
}