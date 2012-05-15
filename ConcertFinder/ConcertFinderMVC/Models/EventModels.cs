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

    public class EventItem
    {
        public string Type { get; set; }
        public string Salle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Website { get; set; }
    }

    public class EventsList
    {
        public List<EventItem> Events { get; set; }
        public List<EventItem> Last { get; set; }

        public EventsList()
        {
            Events = new List<EventItem>();
            Last = new List<EventItem>();
        }
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