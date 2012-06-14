﻿using System;
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
            DataAccess.T_User me = BusinessManagement.User.GetUserByPseudo(User.Identity.Name);
            EventDetail detail = new EventDetail();
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
            event_item.TagList = new List<string>();
            /*
            List<T_Tag> taglist = BusinessManagement.Event.getTagListFromEvent(myevent.Id);
            */
            foreach (DataAccess.T_Tag tag  in myevent.T_Tag)
            {
                event_item.TagList.Add(tag.Name);
            }
            List<EventItem> list = new List<EventItem>();
            detail.Item = event_item;
            if (BusinessManagement.Tool.IsAdmin(User.Identity.Name) || BusinessManagement.Tool.IsAdmin(User.Identity.Name))
            {
               
                list = BusinessManagement.Event.GetEventForAdmin(myevent, 10);
               
            }
            else
            {
                list = BusinessManagement.Event.GetListEventByUserTag(myevent, me);
            }
            detail.Events = list;
            return View(detail);
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
                if (form.Id == 0)
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
                else
                {
                    DataAccess.T_User user = new T_User();
                    user = BusinessManagement.User.GetUserByPseudo(User.Identity.Name);
                    DataAccess.T_Location location = new T_Location ();
                    if ((location = BusinessManagement.Location.GetLocationByCoord(form.Latitude, form.Longitude)) == null)
                    {
                        DataAccess.T_Location location2 = new T_Location();
                        location2.CP = form.CodePostal;
                        location2.Latitude = form.Latitude;
                        location2.Name = form.RoomName;
                        location2.Pays = form.Country;
                        location2.Rue = form.Address;
                        location2.Ville = form.City;
                        BusinessManagement.Location.Create(location2);
                        location = location2;
                    }
                    else
                    {
                        location.CP = form.CodePostal;
                        location.Latitude = form.Latitude;
                        location.Longitude = form.Longitude;
                        location.Name = location.Name;
                        location.Pays = form.Country;
                        location.Rue = form.Address;
                        location.Ville = form.City;
                        BusinessManagement.Location.Update(location);
                    } 
                    if (user != null && BusinessManagement.Event.Update(form, location, user, form.Id))
                    {
                        DataAccess.T_Event bdd_event = BusinessManagement.Event.Get(form.Id, true);
                        BusinessManagement.Notification.Create(user, bdd_event);
                        return RedirectToAction("Detail", "Event", new { id = bdd_event.Id, creation = true });
                    }
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

        public JsonResult AutoComplete(string q)
        {
            List<String> list = BusinessManagement.Tag.Keyword(q);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string q, string type)
        {
            List<String> list = BusinessManagement.Tag.Keyword(q);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
