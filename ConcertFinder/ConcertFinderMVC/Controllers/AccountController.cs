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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogOn(Models.LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                BusinessManagement.SimpleAES encryptor = new BusinessManagement.SimpleAES();
                if (BusinessManagement.User.validate_user(model.Pseudo, encryptor.EncryptToString(model.Password)))
                {
                    FormsAuthentication.SetAuthCookie(model.Pseudo, false);
                }
            }
          // If we got this far, something failed, redisplay form
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Event");
        }

        public ActionResult Register()
        {
            RegisterModel form = new RegisterModel();
            return View(form);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterModel form)
        {
            if (ModelState.IsValid)
            {
                if (BusinessManagement.User.create(form))
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



        public ActionResult Notifications()
        {
            List<DataAccess.T_Notification> notifs = BusinessManagement.Notification.Get(User.Identity.Name);
            List<NotificationItem> notif_items = new List<NotificationItem>();
            foreach (DataAccess.T_Notification notif in notifs)
            {
                NotificationItem item = new NotificationItem()
                {
                    Id = notif.Id,
                    Titre = notif.Titre,
                    Date = notif.Date,
                    Message = notif.Message
                };
                notif_items.Add(item);
            }

            return View(notif_items);
        }
    }
}
