using OrganizerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerProject.ViewModels
{
    public class ListExpenseVM
    {
        public List<Expense> expenseList;
        public Expense expense { get; set; }
    }
}