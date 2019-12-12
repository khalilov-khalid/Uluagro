using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.Areas.AgroPark.ModelViews
{
    public class SiloDetailVM
    {
        public IEnumerable<CROP> crops { get; set; }
        public IEnumerable<CROP_SORT> sorts { get; set; }
        public IEnumerable<CROP_REPRODUCTION> repr { get; set; }
        public MYSILOSTOCK stock { get; set; }
    }
}