using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConcertFinderMVC.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit faire au moins {2} caractères de long !", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "La confirmation de mot de passe n'est pas identique.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength (255, ErrorMessage = "Le {0} doit faire au moins {2} caractères de long.",MinimumLength = 3)]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Pseudo")]
        public string Pseudo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}