using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConcertFinderMVC.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Event/Concert

        public ActionResult Concert()
        {
            return View();
        }

        //
        // GET: /Event/Spectacle

        public ActionResult Spectacle()
        {
            return View();
        }

        //
        // GET: /Event/Festival

        public ActionResult Festival()
        {
            return View();
        }

        //
        // GET: /Event/MyEvents

        public ActionResult MyEvents()
        {
            return View();
        }

    }
}
