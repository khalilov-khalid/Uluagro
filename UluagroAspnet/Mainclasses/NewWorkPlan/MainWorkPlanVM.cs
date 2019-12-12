using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.Mainclasses.NewWorkPlan
{
    public class MainWorkPlanVM
    {
        public List<Worker> Responders { get; set; }
        public List<WORKS_CATEGORY> WorkCategories { get; set; }
        public List<PARCEL_CATEGORY> ParcelCategories { get; set; }
    }
}