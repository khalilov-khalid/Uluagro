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
    
    public partial class WORKPLAN2PARCELS
    {
        public int OBJECTID { get; set; }
        public Nullable<int> PARCELID { get; set; }
        public Nullable<int> WORKPLAN2ID { get; set; }
    
        public virtual PARCEL PARCEL { get; set; }
        public virtual WORKPLAN2 WORKPLAN2 { get; set; }
    }
}