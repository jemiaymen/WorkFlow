using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow.Models
{

    public enum NType
    {
        BesoinCreer = 0,
        BesoinModifierParDemandeur = 1,
        DemandeCreer = 2,
        BesoinRefuser = 3,
        DemandeModification = 4,
        DemandeModifierParRespAchat = 5,
        BonCommande = 10
    }

    public enum Type
    {
        Besoin = 0,
        Demande = 1,
        BonCommande = 2,
        BonLivraison = 3
    }

    public enum State
    {
        Wait = 0,
        Repond = 1,
        Chose = 2
    }

    public class Fournisseur
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nom de fournisseur")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Nom_frn { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Adress_frn { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Mail")]
        public string Mail_frn { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [Phone]
        public string Tel_frn { get; set; }

    }

    public class Category
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Libeler Category")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Index("IndexLblCategory", Order = 1, IsUnique = true )]
        public string Lbl { get; set; }

    }

    public class CategoryInFournisseur
    {
        [Key]
        [Column(Order = 0)]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [Key]
        [Column(Order = 1)]
        public int FournisseurID { get; set; }
        public virtual Fournisseur Fournisseur { get; set; }
    }

    public class Department
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Departement")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Dep { get; set; }

        [Required]
        [Display(Name = "Budget")]
        public float Budget { get; set; }

        [Display(Name = "Dépense")]
        public float Depense { get; set; }

    }

    public class Achat
    {
        public int ID { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Désignation")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Des { get; set; }

        [Required]
        [Display(Name = "Catégorie")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Categ { get; set; }

        [Display(Name = "Date d'Achat")]
        public DateTime DtAcha { get; set; }

        [Required]
        [Display(Name = "Création")]
        public bool Creation { get; set; }

        [Required]
        [Display(Name = "Lieux de Livraison")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string LieuLiv { get; set; }

        [Required]
        [Display(Name = "Imputation")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Imp { get; set; }

        [Required]
        [Display(Name = "Quantité")]
        public int Qte { get; set; }

        public Type Type { get; set; }

        public virtual Department Department { get; set; }

        public Achat()
        {
            DtAcha = DateTime.Now;
            Type = Type.Besoin;
        }
    }

    public class Avis
    {
        public int ID { get; set; }

        [ForeignKey("Achat")]
        public int AchatID { get; set; }

        [Required]
        [Display(Name = "Libellé")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Lbl { get; set; }

        [Required]
        [Display(Name = "Code")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Acceptation")]
        public bool Accept { get; set; }

        public virtual Achat Achat { get; set; }

    }

    public class AchaFournisseur
    {
        public int ID { get; set; }

        [ForeignKey("Achat")]
        public int AchatID { get; set; }

        [ForeignKey("Fournisseur")]
        public int FournisseurID { get; set; }

        [Required]
        [Display(Name = "Prix HT")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Remise")]
        public float Remise { get; set; }

        [Required]
        [Display(Name = "Délais de Paiment")]
        public DateTime Dt { get; set; }

        [Display(Name = "Etat")]
        public State State { get; set; }



        public virtual Fournisseur Fournisseur { get; set; }
        public virtual Achat Achat { get; set; }


        public AchaFournisseur()
        {
            State = State.Wait;
        }
    }

    public class Notification
    {
        public int ID { get; set; }

        [ForeignKey("Achat")]
        public int AchatID { get; set; }

        [Required]
        [Display(Name = "Libellé")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Lbl { get; set; }


        [Required]
        [Display(Name = "Date Notification")]
        public DateTime Dt { get; set; }


        [Display(Name = "Type")]
        public NType Type { get; set; }

        [Required]
        [Display(Name = "Traiter")]
        public bool Etat { get; set; }

        public virtual Achat Achat { get; set; }

        public Notification()
        {
            Dt = DateTime.Now;
            Etat = false;
        }
    }

}