using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcertFinderMVC.Models
{
    public enum Type
    {
        Spectacle = 0,
        Concert,
        Festival
    }

    public class FormEventModels
    {
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }



        [Required]
        [Display(Name = "Date de début")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Date de fin")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Required]
        [Display(Name = "Pays")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Ville")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Code postal")]
        public string CodePostal { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Nom de salle")]
        public string RoomName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Display(Name = "Site web")]
        public string Website { get; set; }
    }
}