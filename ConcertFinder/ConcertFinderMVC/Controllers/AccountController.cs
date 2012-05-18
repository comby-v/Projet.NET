using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
              /*  if (Membership.ValidateUser(model.Pseudo, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);*/
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
    }
}
