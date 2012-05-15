﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcertFinderMVC.Models;

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
                Last = BusinessManagement.T_Event.GetListLastAddEvent(5),
                Events = BusinessManagement.T_Event.GetListEvent(10)
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Concert

        public ActionResult Concert()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.T_Event.GetListLastAddEvent(5),
                Events = BusinessManagement.T_Event.GetListEvent(10, "Concert")
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Spectacle

        public ActionResult Spectacle()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.T_Event.GetListLastAddEvent(5),
                Events = BusinessManagement.T_Event.GetListEvent(10, "Spectacle")
            };
            return View("Index", eventList);
        }

        //
        // GET: /Event/Festival

        public ActionResult Festival()
        {
            EventsList eventList = new EventsList()
            {
                Last = BusinessManagement.T_Event.GetListLastAddEvent(5),
                Events = BusinessManagement.T_Event.GetListEvent(10, "Festival")
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

        public ActionResult Detail()
        {
            return View();
        }
    }
}
