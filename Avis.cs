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
    
    public partial class Avis
    {
        public int ID { get; set; }
        public int AchatID { get; set; }
        public string Lbl { get; set; }
        public int Code { get; set; }
        public bool Accept { get; set; }
    
        public virtual Achats Achats { get; set; }
    }
}
