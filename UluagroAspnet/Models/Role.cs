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
    
    public partial class Role
    {
        public int Id { get; set; }
        public int Action_id { get; set; }
        public int Group_id { get; set; }
        public bool can_view { get; set; }
        public bool can_update { get; set; }
        public bool can_add { get; set; }
        public bool can_delete { get; set; }
        public bool ownerdata { get; set; }
    
        public virtual Action Action { get; set; }
        public virtual GROUP GROUP { get; set; }
    }
}
