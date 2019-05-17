using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganizerProject.Models
{
    public class Budget
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("BudgetId")]
        public int BudgetId { get; set; }

        [Required]
        [Display(Name = "Wypłata")]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Data wypłaty")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SalaryDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndBudgetDate { get; set; }

        public int UserId { get; set; }
        
        public virtual User user { get; set; }
    }
}