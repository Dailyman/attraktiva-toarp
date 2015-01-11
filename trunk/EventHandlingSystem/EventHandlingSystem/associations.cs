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
            this.associationsinevents = new HashSet<associationsinevents>();
            this.categoriesinassociations = new HashSet<categoriesinassociations>();
            this.members = new HashSet<members>();
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
    
        public virtual communities communities { get; set; }
        public virtual ICollection<associationsinevents> associationsinevents { get; set; }
        public virtual ICollection<categoriesinassociations> categoriesinassociations { get; set; }
        public virtual ICollection<members> members { get; set; }
    }
}
