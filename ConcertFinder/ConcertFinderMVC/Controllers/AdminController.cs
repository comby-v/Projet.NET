using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUser()
        {
            /*if (BusinessManagement.User.GetUserByPseudo(User.Identity.Name).Role.Equals(UserModel.GetRoleType((int)eRole.Admin)))
            {*/
                AdminModels admin = new AdminModels();
                admin.User = ConcertFinderMVC.BusinessManagement.User.UserToUserItem(User.Identity.Name);
                admin.listPost = new List<string>() { UserModel.GetRoleType((int)eRole.User), UserModel.GetRoleType((int)eRole.Admin), UserModel.GetRoleType((int)eRole.Moderateur)};
                admin.listUser = BusinessManagement.User.GetListUser();
                return View("AdminUser", admin);
            /*}
            else
            {
                return RedirectToAction("Index", "Event");
            }*/
        }

        public ActionResult BlockUser(long Id)
        {
           BusinessManagement.User.BlockUser(Id);        
            return RedirectToAction("ManageUser");
        }

        public ActionResult NotValidEvent()
        {
            AdminModels admin = new AdminModels();
            admin.User = ConcertFinderMVC.BusinessManagement.User.UserToUserItem(User.Identity.Name);
            admin.listEvent = BusinessManagement.Event.GetListNonValid();
            admin.Page = "Evenement en attente";
            return View("AdminEvent", admin);
        }

        public ActionResult ValidEvent()
        {
            AdminModels admin = new AdminModels();
            admin.User = ConcertFinderMVC.BusinessManagement.User.UserToUserItem(User.Identity.Name);
            admin.listEvent = BusinessManagement.Event.GetListValid();
            admin.Page = "Evenement Valides";
            return View("AdminEvent", admin);
        }

        public ActionResult DenyEvent(long idevent)
        {
            ForbidForm model = new ForbidForm() { IdEvent = idevent};
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DenyEvent(ForbidForm form)
        {
            if (ModelState.IsValid)
            {
                if (BusinessManagement.Notification.Deny(form))
                {
                    return RedirectToAction("NotValidEvent", "Admin");
                }
                else
                {
                    return View("Error");
                }
            }
            return View(form);
        }


        [HttpPost]
        public void ChangeRole(long id, string role)
        {
            BusinessManagement.User.ChangeRole(id, role);
        }
    }

}
