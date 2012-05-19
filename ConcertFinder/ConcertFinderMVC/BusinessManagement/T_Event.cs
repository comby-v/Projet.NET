using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.Models;
using ConcertFinderMVC.DataAccess;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_Event
    {
        static public bool Create(FormEventModels myevent, USER user)
        {
            EVENT ev = new EVENT ();
            ev.EVENT_TITRE = myevent.Title;
           // ev.EVENT_TYPE = myevent.Type;
            ev.EVENT_DESCRIPTION = myevent.Description;
            ev.EVENT_DATEDEBUT = myevent.StartDate;
            ev.EVENT_DATEFIN = myevent.EndDate;
            ev.EVENT_IMG_PATH = myevent.Image;
            ev.EVENT_EMAIL = myevent.Email;
            ev.EVENT_SITE = myevent.Website;
            ev.EVENT_TEL = myevent.Phone;


            //ev.LOCATION = DataAccess.getMapCoord (ev.LOCATION);
            /*
             if (pas d'existant)
             {*/
            ev.LOCATION = new LOCATION();
            ev.LOCATION.LOCATION_PAYS = myevent.Country;
            ev.LOCATION.LOCATION_VILLE = myevent.City;
            ev.LOCATION.LOCATION_CP = myevent.CodePostal;
            ev.LOCATION.LOCATION_RUE = myevent.Address;
            ev.LOCATION.LOCATION_NAME = myevent.RoomName;
            ev.USER = user;
            //}*/
            return DataAccess.T_Event.Create(ev);
        }

        static public bool Delete(long id)
        {
            return DataAccess.T_Event.Delete (id);
        }



        static public bool Update(FormEventModels myevent, LOCATION location, USER user, long id)
        {
            EVENT ev = new EVENT ();
            ev.EVENT_ID = id;
            ev.EVENT_TITRE = myevent.Title;
          //  ev.EVENT_TYPE = myevent.Type;
            ev.EVENT_DESCRIPTION = myevent.Description;
            ev.EVENT_DATEDEBUT = myevent.StartDate;
            ev.EVENT_DATEFIN = myevent.EndDate;
            ev.EVENT_IMG_PATH = myevent.Image;
            ev.EVENT_EMAIL = myevent.Email;
            ev.EVENT_SITE = myevent.Website;
            ev.EVENT_TEL = myevent.Phone;
            ev.LOCATION = location;
            ev.USER = user;
            return DataAccess.T_Event.Update(ev);
        }

        static public EventItem Get(long id)
        {
            return null;
        }

        static public List<EventItem> GetListEvent(int nbr, string type = "")
        {
            List<EventItem> listevent = new List<EventItem>();
            /*DataAccess.T_Event*/
            return listevent;
        }

        static public List<EventItem> GetListLastAddEvent(int nbr, string type = "")
        {
            List<EventItem> listevent = new List<EventItem>();
            /*DataAccess.T_Event*/
            return listevent;
        }
    }
}