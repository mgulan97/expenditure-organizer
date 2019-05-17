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
    public class ExpenseController : Controller
    {
        private OrganizerDBContext db = new OrganizerDBContext();

        public ActionResult Create()
        {
            CreateExpenseVM createExpenseVM = new CreateExpenseVM();
            return View(createExpenseVM);
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            if (ModelState.IsValidField("Typ") && ModelState.IsValidField("Nazwa wydatku") && ModelState.IsValidField("Data zrealizowania wydatku"))
            {
                expense.UserId = int.TryParse(Session["UserId"].ToString(), out var result) ? result : -1;
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("List", "Expense");
            }
            return View(expense);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<Expense> expenseList = db.GetExpenseList();
                Expense expense = expenseList.Where(a => a.ExpensesId == id).Single();
                EditExpenseVM editVM = new EditExpenseVM();
                editVM.expense = expense;
                return View("Edit", editVM);
            }
        }

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                
                    List<Expense> expenseList = db.GetExpenseList();

                    Expense exp = expenseList.Where(a => a.ExpensesId == expense.ExpensesId).Single();
                    exp.Cost = expense.Cost;
                    exp.Descriptions = expense.Descriptions;
                    exp.Name = expense.Name;
                    exp.ReleaseDate = expense.ReleaseDate;
                    exp.Type = expense.Type;

                    db.SaveChanges();


                    List<ListExpenseVM> listVM = new List<ListExpenseVM>();
                    foreach (Expense e in db.GetExpenseList())
                    {
                        ListExpenseVM lVM = new ListExpenseVM();
                        lVM.expense = e;
                        listVM.Add(lVM);
                    }
                
                
                return RedirectToAction("List", "Expense");
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
                if (ModelState.IsValidField("Typ") && ModelState.IsValidField("Nazwa wydatku") && ModelState.IsValidField("Data zrealizowania wydatku") && ModelState.IsValidField("Koszt wydatku"))
                {
                    List<Expense> expenseList = db.GetExpenseList();
                    Expense expense = expenseList.Where(a => a.ExpensesId == id).Single();
                    Deleteexpense(expense);
                    return RedirectToAction("List", "Expense");
                }
                return View();
            }
        }

        public void Deleteexpense(Expense expense)
        {
            db.Expenses.Remove(expense);
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
                List<Expense> expenseList = db.GetExpenseList();
                string currentUser = Session["UserId"].ToString();
                int uid = Int32.Parse(currentUser);
                var expense = expenseList.Where(a => a.UserId == uid).OrderByDescending(a => a.ReleaseDate).ToList();
                ListExpenseVM listExpenseVM = new ListExpenseVM();
                listExpenseVM.expenseList = expense;
                return View("List", listExpenseVM);

            }
        }

        //private List<ExpenseVM> ExpenseList2ExpenseVMList(List<Expense> expenseList)
        //{

        //    List<ExpenseVM> expenseVMList = new List<ExpenseVM>();

        //    foreach (Expense expense in db.GetExpenseList())
        //    {
        //        ExpenseVM expenseVM = new ExpenseVM();
        //        expenseVM.expense = expense;
        //        expenseVMList.Add(expenseVM);
        //    }
        //    return expenseVMList;
        //}
    }
}