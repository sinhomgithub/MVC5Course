using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel data)
        {
            if (CheckLogin(data))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Email, false);
                return RedirectToAction("Index", "Home");
            }            

            return View();
        }

        private bool CheckLogin(LoginViewModel data)
        {
            if(data.Password == "123456")
            {
                return true;
            }
            return false;
        }

        [AllowAnonymous]
        public ActionResult Logout()
        { 
            FormsAuthentication.SignOut();

            return RedirectToAction("Index","Home");
        }



        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel data)
        {
            return View();

        }

       
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel data)
        {
            return View();
        }



    }
}