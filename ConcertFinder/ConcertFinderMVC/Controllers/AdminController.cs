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
            /*if (DataAccess.User.GetUserByPseudo(User.Identity.Name).Role.Equals(UserModel.GetRoleType((int)eRole.Admin)))
            {*/
                AdminModels admin = new AdminModels();
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
            admin.listEvent = BusinessManagement.Event.GetListNonValid();
            return View("AdminEvent", admin);
        }

        public ActionResult ValidEvent()
        {
            AdminModels admin = new AdminModels();
            admin.listEvent = BusinessManagement.Event.GetListValid();
            return View("AdminEvent", admin);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult forbid(ForbidForm form)
        {
            NotificationItem notif = new NotificationItem();

            notif.Date = DateTime.Now;
            notif.Titre = form.Title;
            notif.Message = form.resaon;

            BusinessManagement.Notification.Create();
            return RedirectToAction("NotValidEvent", "Admin");
        }


        [HttpPost]
        public void ChangeRole(long id, string role)
        {
            BusinessManagement.User.ChangeRole(id, role);
        }
    }

}
