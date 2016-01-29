using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmStore0906.Insfrastructure.Abstract;
using FarmStore0906.Models;
using System.Web.Security;
using FarmStore0906.Insfrastructure.Concrete;

namespace FarmStore0906.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider=new FormsAuthProvider(); 
          
        // GET: Account
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
    }
}