using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OrganizerProject.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity),Column("UserId")]
        public int UserId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Podaj adres email!")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Podaj hasło!")]
        [MembershipPassword(
         MinRequiredNonAlphanumericCharacters = 1,
         MinNonAlphanumericCharactersError = "Twoje hasło musi zawierać co najmniej jeden symbol (!, @, #, etc).",
         ErrorMessage = "Twoje hasło musi składać się z 8 znaków i zawierać co najmniej jeden symbol (!, @, #, etc).",
         MinRequiredPasswordLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required(ErrorMessage = "Hasła są różne!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Wiek")]
        [Range(1, 100)]
        [Required(ErrorMessage = "Proszę wprowadzić liczbę z przedziału 1-100!")]
        public int Age { get; set; }

        public virtual ICollection<Budget> Budget { get; set; }

        public virtual ICollection<Expense> Expense { get; set; }
       

    }
}