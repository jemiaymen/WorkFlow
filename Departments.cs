//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkFlow
{
    using System;
    using System.Collections.Generic;
    
    public partial class Departments
    {
        public Departments()
        {
            this.Achats = new HashSet<Achats>();
            this.AspNetUsers = new HashSet<AspNetUsers>();
        }
    
        public int ID { get; set; }
        public string Dep { get; set; }
        public float Budget { get; set; }
        public float Depense { get; set; }
    
        public virtual ICollection<Achats> Achats { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}