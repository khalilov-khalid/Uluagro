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
    
    public partial class WORKPLAN2TASKS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WORKPLAN2TASKS()
        {
            this.WORKPLAN2TASKFELTILIZER = new HashSet<WORKPLAN2TASKFELTILIZER>();
            this.WORKPLAN2TASKQUEUE = new HashSet<WORKPLAN2TASKQUEUE>();
        }
    
        public int OBJECTID { get; set; }
        public Nullable<System.DateTime> STARDATE { get; set; }
        public Nullable<System.DateTime> ENDDATE { get; set; }
        public Nullable<bool> ISDONE { get; set; }
        public Nullable<System.DateTime> FINISHDATE { get; set; }
        public Nullable<int> WORKPLAN2ID { get; set; }
    
        public virtual WORKPLAN2 WORKPLAN2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORKPLAN2TASKFELTILIZER> WORKPLAN2TASKFELTILIZER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORKPLAN2TASKQUEUE> WORKPLAN2TASKQUEUE { get; set; }
    }
}
