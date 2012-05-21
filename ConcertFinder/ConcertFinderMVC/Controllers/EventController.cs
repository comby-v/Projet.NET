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
            DataAccess.Event myevent = BusinessManagement.Event.Get(id);
            EventItem event_item = new EventItem()
            {
                Id = myevent.EVENT_ID,
                StartDate = myevent.EVENT_DATEDEBUT,
                EndDate = myevent.EVENT_DATEFIN.GetValueOrDefault(),
                Description = myevent.EVENT_DESCRIPTION,
                Titre = myevent.EVENT_TITRE,
                Type = myevent.EVENT_TYPE,
                Image = myevent.EVENT_IMG_PATH,
                Email = myevent.EVENT_EMAIL,
                Tel = myevent.EVENT_TEL,
                Website = myevent.EVENT_SITE,
                Salle = myevent.T_Location.LOCATION_NAME,
                Ville = myevent.T_Location.LOCATION_VILLE,
                Pays = myevent.T_Location.LOCATION_PAYS,
                Rue = myevent.T_Location.LOCATION_RUE,
                CP = myevent.T_Location.LOCATION_CP,
                Latitude = myevent.T_Location.LOCATION_LATTITUDE.GetValueOrDefault(),
                Longitude = myevent.T_Location.LOCATION_LONGITUDE.GetValueOrDefault()
                

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
