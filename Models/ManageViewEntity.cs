using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlow.Models
{
    public class DemandeModification
    {
        [Required]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "Modification")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Désignation")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Des { get; set; }

        [Required]
        [Display(Name = "Modification")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Msg { get; set; }
    }
}