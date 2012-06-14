using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConcertFinderMVC.Models
{
    public class EventItem
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Titre { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Website { get; set; }
        public bool Valide { get; set; }
        
        /* Location */
        public string Salle { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string Rue { get; set; }
        public string CP { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class EventsList
    {
        public List<EventItem> Events { get; set; }
        public List<EventItem> Last { get; set; }
    }

    public class EventDetail
    {
        public EventItem Item { get; set; }
        public List<EventItem> Events { get; set; }
    }

    public enum eTypes
    {
        Spectacle = 0,
        Concert,
        Festival
    }

    public class EventModel
    {
        public static List<int> EventTypes = new List<int>()
        {
            (int)eTypes.Spectacle,
            (int)eTypes.Concert,
            (int)eTypes.Festival,
        };

        public static string GetEventType(int type)
        {
            var enumType = (eTypes)type;
            switch (enumType)
            {
                case eTypes.Spectacle:
                    return "Spectacle";
                case eTypes.Concert:
                    return "Concert";
                case eTypes.Festival:
                    return "Festival";
                default:
                    return string.Empty;
            }
        }

        public static eTypes GetEventType(string type)
        {
            switch (type)
            {
                case "Spectacle":
                    return eTypes.Spectacle;
                case "Concert":
                    return eTypes.Concert;
                case "Festival":
                    return eTypes.Festival;
                default:
                    return eTypes.Spectacle;
            }
        }

        public static Dictionary<int, string> GetEventTypes()
        {
            return EventTypes.ToDictionary(o => o, o => GetEventType(o));
        }

        public static Dictionary<int, string> GetTypesDic(IEnumerable<eTypes> except)
        {
            var toRet = (from item in GetEventTypes() where !except.Contains((eTypes)item.Key) select item).ToDictionary(k => k.Key, k => k.Value);
            return toRet;
        }
    }

    public class FormEventModels
    {
        public FormEventModels()
        {
            var types = EventModel.GetTypesDic(new List<eTypes>());
            Types = new SelectList(types, "Key", "Value", eTypes.Concert);
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now;
        }

        public long Id { get; set; }

        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Display(Name = "Type")]
        public SelectList Types { get; set; }

        public int Type { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date de début")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date de fin")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase FileImage { get; set; }

        public String Image { get; set; }

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

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Site web")]
        public string Website { get; set; }
    }
}