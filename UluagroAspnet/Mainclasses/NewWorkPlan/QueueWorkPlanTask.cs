using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.Mainclasses.NewWorkPlan
{
    public class QueueWorkPlanTask
    {
        public int QueueCount { get; set; }
        public List<Profession> WorkerProfessions { get; set; }
    }
}