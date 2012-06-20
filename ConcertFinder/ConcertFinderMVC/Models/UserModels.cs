using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ConcertFinderMVC.Models
{
    public enum eRole
    {
        User = 0,
        Admin,
        Moderateur
    }

    public class UserModel
    {
        public static string GetRoleType(int type)
        {
            var enumType = (eRole)type;
            switch (enumType)
            {
                case eRole.User:
                    return "Utilisateur";
                case eRole.Admin:
                    return "Administrateur";
                case eRole.Moderateur:
                    return "Moderateur";
                default:
                    return string.Empty;
            }
        }
    }

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

        [Display(Name = "Se souvenir de moi")]
        public bool RemembreMe { get; set; }
    }

    public class ParameterModel
    {
        [Required]
        [Display(Name = "Ma ville")]
        public string MyCity { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Tags")]
        public string Tag { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit être au moins de {2} caractères de long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et la confirmation ne sont pas identiques.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class UserItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public bool Deleted { get; set; }
    }
}