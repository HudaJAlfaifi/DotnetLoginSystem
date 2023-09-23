using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class AccountsController : Controller
    {

        DBnewEntities entity = new DBnewEntities(); 
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel credentials)
        {
            bool userExist =
                entity.UsersTbls.Any(x => x.Email == credentials.Email && x.Passcod == credentials.Password);
            UsersTbl u =
                entity.UsersTbls.FirstOrDefault(x => x.Email == credentials.Email && x.Passcod == credentials.Password);

            if (userExist) 
            {

                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index", "Home");
            
            }

            ModelState.AddModelError("", "USER Name Or Password Is Wrong");

            return View();
        }
        [HttpPost]
        public ActionResult Signup(UsersTbl userinfo)
        {
            entity.UsersTbls.Add(userinfo);
            entity.SaveChanges();
            return RedirectToAction("Login");
          
        }


        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}