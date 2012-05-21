using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.Models;
using ConcertFinderMVC.DataAccess;
using System.Text.RegularExpressions;

namespace ConcertFinderMVC.BusinessManagement
{
    public class T_Event
    {
        static public bool Create(FormEventModels myevent, USER user)
        {
            EVENT ev = new EVENT ();
            ev.EVENT_TITRE = myevent.Title;
            ev.EVENT_TYPE = EventModel.GetEventType(myevent.Type);
            ev.EVENT_DESCRIPTION = myevent.Description;
            ev.EVENT_DATEDEBUT = myevent.StartDate;
            ev.EVENT_DATEFIN = myevent.EndDate;
            ev.EVENT_IMG_PATH = myevent.Image;
            ev.EVENT_EMAIL = myevent.Email;
            ev.EVENT_SITE = myevent.Website;
            ev.EVENT_TEL = myevent.Phone;

            List<TAG> list_tag = new List<TAG>();
            List<String> tags = myevent.Tags.Split(new Char[] { ' ', ',', '.', ';'}).ToList();
            foreach (String tag in tags)
            {
                Regex regx = new Regex("[a-z1-9*]");
                if (tag.Length > 2 && regx.Match(tag).Success)
                {
                    tag.ToLower();
                    TAG bdd_tag = BusinessManagement.T_Tag.Get(tag);
                    if (bdd_tag != null)
                    {
                        list_tag.Add(bdd_tag);
                    }
                    else
                    {
                        BusinessManagement.T_Tag.Create(tag);
                        bdd_tag = BusinessManagement.T_Tag.Get(tag);
                        list_tag.Add(bdd_tag);
                    }
                }
            }

            LOCATION location = T_Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
            if (location != null)
            {
                return DataAccess.T_Event.Create(ev, user, location, list_tag);
            }
            else
            {
                if (BusinessManagement.T_Location.Create(new LOCATION()
                                                        {
                                                            LOCATION_PAYS = myevent.Country,
                                                            LOCATION_VILLE = myevent.City,
                                                            LOCATION_CP = myevent.CodePostal,
                                                            LOCATION_RUE = myevent.Address,
                                                            LOCATION_NAME = myevent.RoomName,
                                                            LOCATION_LATTITUDE = myevent.Latitude,
                                                            LOCATION_LONGITUDE = myevent.Longitude
                                                        }))
                {
                    LOCATION n_location = T_Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
                    return DataAccess.T_Event.Create(ev, user, n_location, list_tag);
                }
                else
                {
                    return false;
                }
            }
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
            ev.EVENT_TYPE = EventModel.GetEventType(myevent.Type);
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

        static public EVENT Get(long id)
        {
            return DataAccess.T_Event.Get(id);
        }

        static public EVENT Get(string title)
        {
            return DataAccess.T_Event.Get(title);
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