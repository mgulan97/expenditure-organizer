using OrganizerProject.DAL;
using OrganizerProject.Helper;
using OrganizerProject.Models;
using OrganizerProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace OrganizerProject.Controllers
{
  
    public class UserController : Controller
    {
        private OrganizerDBContext db = new OrganizerDBContext();
        private PasswordHelper helper = new PasswordHelper();

        // GET: User
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                List<User> userList = db.GetUserList();
                List<Budget> budgetList = db.GetBudgetList();
                List<Expense> expenseList = db.GetExpenseList();

                string currentUser = Session["UserId"].ToString();
                int uid = Int32.Parse(currentUser);
                User user = userList.Where(a => a.UserId == uid).Single();
                //Wyświetlenie 10 ostatnio dodanych wydatków
                var expense= expenseList.Where(a => a.UserId == uid).OrderByDescending(a => a.ReleaseDate).Take(10).ToList();
                //Ostatnio dodany budżet
                Budget lastBudget = budgetList.Where(b => b.UserId == uid).OrderByDescending(b => b.BudgetId).FirstOrDefault();
                decimal allBudgets = budgetList.Where(b => b.UserId == uid).Sum(b => b.Salary);

                //Dane do wykresu
                try
                {
                    decimal billExpense = expenseList.Where(x => x.Type == Expense.ExpenseType.Rachunek && x.UserId == uid).Sum(x => x.Cost);
                    decimal shoppingExpense = expenseList.Where(x => x.Type == Expense.ExpenseType.Zakupy && x.UserId == uid).Sum(x => x.Cost);
                    decimal departureExpense = expenseList.Where(x => x.Type == Expense.ExpenseType.Wyjścia && x.UserId == uid).Sum(x => x.Cost);
                    var list = Enum.GetValues(typeof(Expense.ExpenseType)).Cast<Expense.ExpenseType>().ToList();
                    decimal allExpense = billExpense + shoppingExpense + departureExpense;
                    decimal _value = allBudgets - allExpense;
                    ViewBag.Message = _value;
               
            
                var myChart = new Chart(width: 600, height: 500)
                .AddTitle("Wydatki")
                .AddSeries(
                name: "Wydatki",
                chartType: "column",
                xValue: new[] { "Rachunki", "Zakupy", "Wyjścia" },
                yValues: new[] { billExpense, shoppingExpense, departureExpense});
                myChart.Save("~/Content/chart.png");
                }
                catch { }

                //Przekazanie do viewmodelu
                DetailsVM detailsVM = new DetailsVM();
                detailsVM.user = user;
                detailsVM.budget = lastBudget;
                detailsVM.expense = expense;

                return View("Index", detailsVM);
               
            }

        }

      
        public ActionResult Create()
        {
            User user = new User();
            return View(user);
        }
      
        [HttpPost]
        public ActionResult Create(User user)
        {
           
            if (ModelState.IsValid)
            {
                string data1 = user.Password;
                string data2 = user.ConfirmPassword;
                user.Password = helper.EncryptedPassword(data1, "h@sheD");
                user.ConfirmPassword = helper.EncryptedPassword(data2, "h@sheD");
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                string currentUser = Session["UserId"].ToString();
                int uid = Int32.Parse(currentUser);
                List<User> userList = db.GetUserList();
                User user = userList.Where(a => a.UserId == uid).Single();
                string data1 = user.Password;
                string data2 = user.ConfirmPassword;
               
                user.Password = helper.DecryptedPassword(data1, "h@sheD");
                user.ConfirmPassword = helper.DecryptedPassword(data2, "h@sheD");
                EditUserVM editVM = new EditUserVM();
                editVM.user = user;
                return View("Edit", editVM);
            }


        }

        [HttpPost]
        public ActionResult Edit(User user)
        {

            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                
                    List<User> userList = db.GetUserList();

                    User u = userList.Where(a => a.Email == user.Email).Single();
                    u.Email = user.Email;
                    u.Password = helper.EncryptedPassword(user.Password, "h@sheD");//user.Password;
                    u.ConfirmPassword = helper.EncryptedPassword(user.ConfirmPassword, "h@sheD");//user.ConfirmPassword;
                    u.Age = user.Age;
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
                    db.SaveChanges();

                    List<ListUserVM> listVM = new List<ListUserVM>();
                    foreach (User usr in db.GetUserList())
                    {
                        ListUserVM lVM = new ListUserVM();
                        lVM.user = usr;
                        listVM.Add(lVM);
                    }
                    return RedirectToAction("Index", "User");
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
                if(ModelState.IsValidField("Email") && ModelState.IsValidField("Imię") && ModelState.IsValidField("Nazwisko") && ModelState.IsValidField("Wiek"))
                {
                    string currentUser = Session["UserId"].ToString();
                    int uid = Int32.Parse(currentUser);
                    List<User> userList = db.GetUserList();
                    User user = userList.Where(a => a.UserId == uid).Single();
                    DeleteUserVM deleteVM = new DeleteUserVM();
                    deleteVM.user = user;
                    return View("Delete", deleteVM);
                }

                return View();
            }
           
        }


        public void Deleteuser(User user)
        {
                db.Users.Attach(user);
                db.Users.Remove(user);
                db.SaveChanges();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                if(ModelState.IsValidField("Email") && ModelState.IsValidField("Imię") && ModelState.IsValidField("Nazwisko") && ModelState.IsValidField("Wiek"))
                {
                    string currentUser = Session["UserId"].ToString();
                    int uid = Int32.Parse(currentUser);
                    List<User> userList = db.GetUserList();
                    User user = userList.Where(a => a.UserId == uid).Single();
                    Deleteuser(user);
                    return RedirectToAction("Logout", "Authentication");
                }

                return View();
            }
        }

        

    }
}
    
