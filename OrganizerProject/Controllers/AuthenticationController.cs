using OrganizerProject.DAL;
using OrganizerProject.Helper;
using OrganizerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OrganizerProject.Controllers
{
    public class AuthenticationController : Controller
    {
        PasswordHelper helper = new PasswordHelper();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email, Password")]User model)
        {
            string result = helper.EncryptedPassword(model.Password, "h@sheD");
            
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Hasło"))
            {
                using (OrganizerDBContext db = new OrganizerDBContext())
                {
                    var obj = db.Users.Where(a => a.Email.Equals(model.Email) && a.Password.Equals(result)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.Email.ToString();
                        return RedirectToAction("Index", "User");
                    }
                }
            }
            return View(model);
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Login", "Authentication");
        }
    }
}
