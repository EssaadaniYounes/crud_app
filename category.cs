//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crud
{
    using System;
    using System.Collections.Generic;
    
    public partial class category
    {
        public category()
        {
            this.films = new HashSet<film>();
        }
    
        public int code_cat { get; set; }
        public string label { get; set; }
    
        public virtual ICollection<film> films { get; set; }
    }
}