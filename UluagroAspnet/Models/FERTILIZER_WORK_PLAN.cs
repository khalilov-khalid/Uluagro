//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UluagroAspnet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FERTILIZER_WORK_PLAN
    {
        public int OBJECTID { get; set; }
        public Nullable<int> DAYLY_WORK_PLAN_ID { get; set; }
        public Nullable<int> FERTILIZER_ID { get; set; }
        public Nullable<int> WATER_UNIT { get; set; }
        public Nullable<int> FERTILIZER_UNIT { get; set; }
    
        public virtual FERTILIZER FERTILIZER { get; set; }
        public virtual QueuePlan QueuePlan { get; set; }
    }
}