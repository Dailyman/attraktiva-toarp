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
    
    public partial class events
    {
        public events()
        {
            this.associations = new HashSet<associations>();
            this.communities = new HashSet<communities>();
            this.subcategories = new HashSet<subcategories>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Other { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public bool DayEvent { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string TargetGroup { get; set; }
        public Nullable<int> ApproximateAttendees { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime LatestUpdate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<associations> associations { get; set; }
        public virtual ICollection<communities> communities { get; set; }
        public virtual ICollection<subcategories> subcategories { get; set; }
    }
}
