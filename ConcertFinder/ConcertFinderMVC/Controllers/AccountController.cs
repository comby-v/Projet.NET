using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcertFinderMVC.Models;
using System.Web.Security;

namespace ConcertFinderMVC.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(Models.LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                    FormsAuthentication.SetAuthCookie(model.Pseudo, false);
                    var ojb = User.Identity.Name;
                    var bidon = Request.IsAuthenticated;
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Event");
                    }
            }
          // If we got this far, something failed, redisplay form
           return View(model);

        }

        public ActionResult Register()
        {
            RegisterModel form = new RegisterModel();
            return View(form);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel form)
        {
            if (ModelState.IsValid)
            {
                if (BusinessManagement.T_User.create(form))
                {
                    return RedirectToAction("Index", "Event");
                }
                else
                {
                    // TODO : Rediriger vers une page d'erreur
                    return View("Error");
                }
            }
            return View(form);
        }
    }
}
