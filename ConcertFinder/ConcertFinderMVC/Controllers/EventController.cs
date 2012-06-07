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
                Events = BusinessManagement.Event.GetListEvent(5)
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
                Events = BusinessManagement.Event.GetListEvent(5, EventModel.GetEventType((int)eTypes.Concert))
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
                Events = BusinessManagement.Event.GetListEvent(5, EventModel.GetEventType((int)eTypes.Spectacle))
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
                Events = BusinessManagement.Event.GetListEvent(5, EventModel.GetEventType((int)eTypes.Festival))
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/MyEvents

        public ActionResult MyEvents()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.Event.GetListLastAddEvent(5),
                Events = BusinessManagement.Event.MyEvent(User.Identity.Name)
            };
            return View("Index", eventList);
        }

        //
        // GET : /Event/Detail/Id

        public ActionResult Detail(long id, bool creation = false)
        {
            DataAccess.T_Event myevent = BusinessManagement.Event.Get(id, creation);
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
                Valide = myevent.Valide.GetValueOrDefault(),
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

        public ActionResult CreateEvent(long? id)
        {
            FormEventModels form;
            if (id.HasValue)
            {
                T_Event myevent = BusinessManagement.Event.Get(id.Value, true);
                string tags = "";
                foreach (T_Tag tag in myevent.T_Tag.ToList())
                {
                    tags += tag.Name + " ";
                }

                form = new FormEventModels()
                {
                    Id = myevent.Id,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Description = myevent.Description,
                    Title = myevent.Titre,
                    Email = myevent.Email,
                    Phone = myevent.Tel,
                    Website = myevent.WebSite,
                    Type = (int)EventModel.GetEventType(myevent.Type),
                    Tags = tags,

                    RoomName = myevent.T_Location.Name,
                    Country = myevent.T_Location.Pays,
                    City = myevent.T_Location.Ville,
                    CodePostal = myevent.T_Location.CP,
                    Address = myevent.T_Location.Rue,
                    Latitude = myevent.T_Location.Latitude,
                    Longitude = myevent.T_Location.Longitude
                };
            }
            else
            {
                form = new FormEventModels();
            }

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
                    return RedirectToAction("Detail", "Event", new { id = bdd_event.Id, creation = true });
                }
                else
                {
                    // TODO : Rediriger vers une page d'erreur
                    return View("Error");
                }
            }
            return View(form);
        }

        public ActionResult ValideEvent(long idevent)
        {
            BusinessManagement.Event.ValidEvent(idevent);

            return RedirectToAction("NotValidEvent", "Admin");
        }


        public JsonResult GetNextEvents(int last_id, string type)
        {
            List<EventItem> list = BusinessManagement.Event.GetNextEvents(last_id, type);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string q)
        {
            List<String> list = BusinessManagement.Tag.Keyword(q);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
