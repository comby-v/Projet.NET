using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcertFinderMVC.Models;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.Event.GetListLastAddEvent(5),
                Events = BusinessManagement.Event.GetListEvent(10)
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Concert

        public ActionResult Concert()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.Event.GetListLastAddEvent(5),
                Events = BusinessManagement.Event.GetListEvent(10, "Concert")
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Spectacle

        public ActionResult Spectacle()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.Event.GetListLastAddEvent(5),
                Events = BusinessManagement.Event.GetListEvent(10, "Spectacle")
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Festival

        public ActionResult Festival()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.Event.GetListLastAddEvent(5),
                Events = BusinessManagement.Event.GetListEvent(10, "Festival")
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/MyEvents

        public ActionResult MyEvents()
        {
            return View();
        }

        //
        // GET : /Event/Detail/Id

        public ActionResult Detail(long id)
        {


            return View();
        }

        public ActionResult CreateEvent()
        {
            FormEventModels form = new FormEventModels();
            return View(form);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateEvent(FormEventModels form)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                DataAccess.User user = BusinessManagement.User.GetUserByPseudo(User.Identity.Name);
                if (user != null && BusinessManagement.Event.Create(form, user))
                {
                    DataAccess.Event bdd_event = BusinessManagement.Event.Get(form.Title, true);
                    BusinessManagement.Notification.Create(user, bdd_event);
                    return RedirectToAction("Detail", "Event", new { id = bdd_event.EVENT_ID });
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
