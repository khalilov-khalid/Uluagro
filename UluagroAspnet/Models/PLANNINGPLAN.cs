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
    
    public partial class PLANNINGPLAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PLANNINGPLAN()
        {
            this.PLANINGPLANBYDETAILS = new HashSet<PLANINGPLANBYDETAIL>();
        }
    
        public int OBJECTID { get; set; }
        public System.DateTime PLANNINGDATE { get; set; }
        public int PARCELID { get; set; }
        public int CROPSORTID { get; set; }
        public int CROPREPRODUCTIONID { get; set; }
        public System.DateTime CREATEDDATE { get; set; }
        public int SEASON { get; set; }
    
        public virtual CROP_REPRODUCTION CROP_REPRODUCTION { get; set; }
        public virtual CROP_SORT CROP_SORT { get; set; }
        public virtual PARCEL PARCEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLANINGPLANBYDETAIL> PLANINGPLANBYDETAILS { get; set; }
    }
}