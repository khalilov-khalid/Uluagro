using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UluagroAspnet.Models;
using UluagroAspnet.ClassAJAXS;

namespace UluagroAspnet.Mainclasses
{
    public class WorkPlanViewModel
    {        
        public List<TECHNIQUE_TYPE> _TechniqueCategory { get; set; }
        public List<TechnicalArray> _Technique { get; set; }
        public List<CATEGORy> _FeltilizerCAtegory { get; set; }
        public List<FeltilizerArray> _Feltilizer { get; set; }
        public List<PARCEL_CATEGORY> _ParcelCategory { get; set; }
        public List<ParcelArray> _Parcel { get; set; }
        public List<Worker> _Responders { get; set; }
        public List<WorkCategoryArray> _WorkCategory { get; set; }
        public List<WORK> _Work { get; set; }
        public List<ProfessionArray> _Profession { get; set; }
        public List<WorkerArray> _Worker { get; set; }
    }
}