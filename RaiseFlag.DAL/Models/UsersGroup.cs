//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RaiseFlag.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersGroup
    {
        public long ID { get; set; }
        public long GroupID { get; set; }
        public long UserID { get; set; }
        public int RoleID { get; set; }
        public bool Status { get; set; }
        public bool Tracked { get; set; }
        public bool Notified { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
