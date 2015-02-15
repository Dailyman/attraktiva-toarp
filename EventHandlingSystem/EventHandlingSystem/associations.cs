//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventHandlingSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class associations
    {
        public associations()
        {
            this.members = new HashSet<members>();
            this.events = new HashSet<events>();
            this.categories = new HashSet<categories>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentAssociationId { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime LatestUpdate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int Communities_Id { get; set; }
        public string LogoUrl { get; set; }
    
        public virtual communities communities { get; set; }
        public virtual ICollection<members> members { get; set; }
        public virtual ICollection<events> events { get; set; }
        public virtual ICollection<categories> categories { get; set; }
    }
}