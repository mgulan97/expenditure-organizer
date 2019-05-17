using OrganizerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizerProject.ViewModels
{
    public class DetailsVM
    {
        public User user { get; set; }

        public Budget budget { get; set; }

        public List<Expense> expense { get; set; }

     
        [Display(Name = "Imię i nazwisko")]
        public string FullName
        {
            get
            {
                return user.FirstName + " " + user.LastName;
            }
        }

       

        public string AgeColor
        {
            get
            {
                if (user.Age < 18)
                {
                    return "red";
                }
                return "black";
            }
        }
    }
}