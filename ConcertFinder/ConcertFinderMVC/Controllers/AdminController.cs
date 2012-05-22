﻿using System;
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
            if (DataAccess.User.GetUserByPseudo(User.Identity.Name).Role.Equals ("Administrateur"))
            {
                AdminModels admin = new AdminModels();
                admin.listPost = new List<string>() { "Utilisateur", "Administrateur", "Moderateur" };
                admin.listUser = BusinessManagement.User.GetListUser();
                return View("AdminUser", admin);
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult BlockUser(long Id)
        {
           BusinessManagement.User.BlockUser(Id);        
            return RedirectToAction("ManageUser");
        }

        public ActionResult ManageEvent()
        {
            return View("AdminEvent");
        }
    }

}
