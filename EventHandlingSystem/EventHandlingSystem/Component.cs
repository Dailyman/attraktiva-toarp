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
    
    public partial class Component
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Title { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public System.DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Content Content { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Feed Feed { get; set; }
        public virtual Navigation Navigation { get; set; }
    }
}