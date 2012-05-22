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
                Events = BusinessManagement.Event.GetListEvent(10, EventModel.GetEventType((int)eTypes.Concert))
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
                Events = BusinessManagement.Event.GetListEvent(10, EventModel.GetEventType((int)eTypes.Spectacle))
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
                Events = BusinessManagement.Event.GetListEvent(10, EventModel.GetEventType((int)eTypes.Festival))
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
            DataAccess.T_Event myevent = BusinessManagement.Event.Get(id);
            EventItem event_item = new EventItem()
            {
                Id = myevent.Id,
                StartDate = myevent.DateDebut,
                EndDate = myevent.DateFin.GetValueOrDefault(),
                Description = myevent.Description,
                Titre = myevent.Titre,
                Type = myevent.Type,
                Image = myevent.Image,
                Email = myevent.Email,
                Tel = myevent.Tel,
                Website = myevent.WebSite,
                Salle = myevent.T_Location.Name,
                Ville = myevent.T_Location.Ville,
                Pays = myevent.T_Location.Pays,
                Rue = myevent.T_Location.Rue,
                CP = myevent.T_Location.CP,
                Latitude = myevent.T_Location.Latitude,
                Longitude = myevent.T_Location.Longitude,
                

            };

            return View(event_item);
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
                DataAccess.T_User user = BusinessManagement.User.GetUserByPseudo(User.Identity.Name);
                if (user != null && BusinessManagement.Event.Create(form, user))
                {
                    DataAccess.T_Event bdd_event = BusinessManagement.Event.Get(form.Title, true);
                    BusinessManagement.Notification.Create(user, bdd_event);
                    return RedirectToAction("Detail", "Event", new { id = bdd_event.Id });
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
