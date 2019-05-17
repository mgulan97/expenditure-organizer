using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganizerProject.Models
{
    public class Expense
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ExpenseId")]
        public int ExpensesId { get; set; }

        [Required]
        [Display(Name = "Typ")]
        public ExpenseType Type { get; set; }

        [Required]
        [Display(Name = "Nazwa wydatku")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Data zrealizowania wydatku")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Opis wydatku")]
        public string Descriptions { get; set; }

        [Display(Name = "Koszt wydatku")]
        public decimal Cost { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public enum ExpenseType
        {
            Rachunek,
            Zakupy,
            Wyjścia
        }

      
    }
    

}