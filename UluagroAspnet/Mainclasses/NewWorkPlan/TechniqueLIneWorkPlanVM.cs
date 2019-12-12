using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;

namespace UluagroAspnet.Mainclasses.NewWorkPlan
{
    public class TechniqueLIneWorkPlanVM
    {
        public List<TECHNIQUE_TYPE> texCategory { get; set; }
        public List<Worker> drivers { get; set; }
    }
}