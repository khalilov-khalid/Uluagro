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
    
    public partial class WORK_PLAN_TECHNIQUE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WORK_PLAN_TECHNIQUE()
        {
            this.TECHNIQUE_TRAILER = new HashSet<TECHNIQUE_TRAILER>();
        }
    
        public int OBJECTID { get; set; }
        public Nullable<int> DAYLY_WORK_PLAN_ID { get; set; }
        public Nullable<int> TECHNIQUE_ID { get; set; }
        public Nullable<int> DRIVER_ID { get; set; }
    
        public virtual QueuePlan QueuePlan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TECHNIQUE_TRAILER> TECHNIQUE_TRAILER { get; set; }
        public virtual TECHNIQUE TECHNIQUE { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
