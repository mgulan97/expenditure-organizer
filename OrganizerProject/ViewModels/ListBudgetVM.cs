using OrganizerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizerProject.ViewModels
{
    public class ListBudgetVM
    {
        public List<Budget> budgetVMList;
        public Budget budget { get; set; }
    }
}