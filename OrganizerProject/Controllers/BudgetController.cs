using OrganizerProject.DAL;
using OrganizerProject.Models;
using OrganizerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizerProject.Controllers
{
    public class BudgetController : Controller
    {
        private OrganizerDBContext db = new OrganizerDBContext();
      
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                CreateBudgetVM createbudgetVM = new CreateBudgetVM();
                return View(createbudgetVM);
            }
        }
       
        [HttpPost]
        public ActionResult Create(Budget budget)
        {
            
            string currentUser = Session["UserId"].ToString();
            int uid = Int32.Parse(currentUser);
            List<Budget> budgetList = db.GetBudgetList();
            
          
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    budget.UserId = int.TryParse(Session["UserId"].ToString(), out var result) ? result : -1;
                    if (budgetList.Where(a => a.UserId == uid).Count() >= 1)
                    {
                        //Wyciagniecie 2 wstecz
                        //var _budget = budgetList.Where(a => a.UserId == uid).OrderByDescending(a => a.BudgetId).Take(2).OrderBy(a => a.BudgetId).FirstOrDefault();
                        var _budget = budgetList.Where(a => a.UserId == uid).OrderByDescending(a => a.BudgetId).FirstOrDefault();
                        _budget.EndBudgetDate = budget.SalaryDate.Value.AddDays(-1);
                    }
                    db.Budgets.Add(budget);
                    db.SaveChanges();
                    return RedirectToAction("List", "Budget");
                }
                return View(budget);
            }
           
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Budget> budgetList = db.GetBudgetList();
                Budget budget = budgetList.Where(a => a.BudgetId == id).Single();
                EditBudgetVM editVM = new EditBudgetVM();
                editVM.budget = budget;
                return View("Edit", editVM);
            }
        }

        [HttpPost]
        public ActionResult Edit(Budget budget)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Budget> budgetList = db.GetBudgetList();

                Budget b = budgetList.Where(a => a.BudgetId == budget.BudgetId).Single();
                b.Salary = budget.Salary;
                b.SalaryDate = budget.SalaryDate;
                               
                db.SaveChanges();

                List<ListBudgetVM> listVM = new List<ListBudgetVM>();
                foreach (Budget bdg in db.GetBudgetList())
                {
                    ListBudgetVM lVM = new ListBudgetVM();
                    lVM.budget = bdg;
                    listVM.Add(lVM);
                }
                return RedirectToAction("List", "Budget");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if (ModelState.IsValidField("Wypłata") && ModelState.IsValidField("Data wypłaty"))
                {                  
                    List<Budget> budgetList = db.GetBudgetList();
                    Budget budget = budgetList.Where(a => a.BudgetId == id).Single();
                    Deletebudget(budget);                    
                    return RedirectToAction("List", "Budget");
                }
                return View();
            }
        }

        public void Deletebudget(Budget budget)
        {
            db.Budgets.Remove(budget);
            db.SaveChanges();
        }

     
        public ActionResult List()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Budget> budgetList = db.GetBudgetList();
                string currentUser = Session["UserId"].ToString();
                int uid = Int32.Parse(currentUser);
                var budget = budgetList.Where(a => a.UserId == uid).OrderByDescending(a => a.SalaryDate).ToList();
                ListBudgetVM listBudgetVM = new ListBudgetVM();
                listBudgetVM.budgetVMList = budget;
                return View("List", listBudgetVM);
            }
        }

      
    }
}