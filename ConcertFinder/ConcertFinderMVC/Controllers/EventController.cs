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

    }
}
