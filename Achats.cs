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
    
    public partial class Achats
    {
        public Achats()
        {
            this.AchaFournisseurs = new HashSet<AchaFournisseurs>();
            this.Avis = new HashSet<Avis>();
            this.Notifications = new HashSet<Notifications>();
        }
    
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public string Des { get; set; }
        public string Categ { get; set; }
        public System.DateTime DtAcha { get; set; }
        public bool Creation { get; set; }
        public string LieuLiv { get; set; }
        public string Imp { get; set; }
        public int Qte { get; set; }
        public int Type { get; set; }
    
        public virtual ICollection<AchaFournisseurs> AchaFournisseurs { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual ICollection<Avis> Avis { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
