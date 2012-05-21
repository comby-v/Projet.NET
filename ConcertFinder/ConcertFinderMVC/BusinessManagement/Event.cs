using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.Models;
using ConcertFinderMVC.DataAccess;
using System.Text.RegularExpressions;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Event
    {
        static public bool Create(FormEventModels myevent, DataAccess.User user)
        {
            DataAccess.Event ev = new DataAccess.Event ();
            ev.EVENT_TITRE = myevent.Title;
            ev.EVENT_TYPE = EventModel.GetEventType(myevent.Type);
            ev.EVENT_DESCRIPTION = myevent.Description;
            ev.EVENT_DATEDEBUT = myevent.StartDate;
            ev.EVENT_DATEFIN = myevent.EndDate;
            ev.EVENT_IMG_PATH = myevent.Image;
            ev.EVENT_EMAIL = myevent.Email;
            ev.EVENT_SITE = myevent.Website;
            ev.EVENT_TEL = myevent.Phone;

            List<DataAccess.Tag> list_tag = new List<DataAccess.Tag>();
            List<String> tags = myevent.Tags.Split(new Char[] { ' ', ',', '.', ';'}).ToList();
            foreach (String tag in tags)
            {
                Regex regx = new Regex("[a-z1-9*]");
                if (tag.Length > 2 && regx.Match(tag).Success)
                {
                    tag.ToLower();
                    DataAccess.Tag bdd_tag = BusinessManagement.Tag.Get(tag);
                    if (bdd_tag != null)
                    {
                        list_tag.Add(bdd_tag);
                    }
                    else
                    {
                        BusinessManagement.Tag.Create(tag);
                        bdd_tag = BusinessManagement.Tag.Get(tag);
                        list_tag.Add(bdd_tag);
                    }
                }
            }

            DataAccess.Location location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
            if (location != null)
            {
                return DataAccess.Event.Create(ev, user, location, list_tag);
            }
            else
            {
                if (BusinessManagement.Location.Create(new DataAccess.Location()
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
                    DataAccess.Location n_location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
                    return DataAccess.Event.Create(ev, user, n_location, list_tag);
                }
                else
                {
                    return false;
                }
            }
        }

        static public bool Delete(long id)
        {
            return DataAccess.Event.Delete (id);
        }


        static public bool Update(FormEventModels myevent, DataAccess.Location location, DataAccess.User user, long id)
        {
            DataAccess.Event ev = new DataAccess.Event();
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
            ev.T_Location = location;
            ev.T_User = user;

            return DataAccess.Event.Update(ev);
        }

        static public DataAccess.Event Get(long id)
        {
            return DataAccess.Event.Get(id);
        }

        static public DataAccess.Event Get(string title, bool creation = false)
        {
            return DataAccess.Event.Get(title, creation);
        }

        static public List<EventItem> GetListLastAddEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.Event> list_event = DataAccess.Event.GetListLastAddEvent(nbr, type);
            foreach (DataAccess.Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.EVENT_ID,
                    Titre = myevent.EVENT_TITRE,
                    Description = myevent.EVENT_DESCRIPTION,
                    Type = myevent.EVENT_TYPE,
                    StartDate = myevent.EVENT_DATEDEBUT,
                    EndDate = myevent.EVENT_DATEFIN.GetValueOrDefault(),
                    Salle = myevent.T_Location.LOCATION_NAME,
                    Image = myevent.EVENT_IMG_PATH,
                    Email = myevent.EVENT_EMAIL,
                    Tel = myevent.EVENT_TEL,
                    Website = myevent.EVENT_SITE
                };
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }


        static public List<EventItem> GetListEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.Event> list_event = DataAccess.Event.GetListEvent(nbr, type);
            foreach (DataAccess.Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.EVENT_ID,
                    Titre = myevent.EVENT_TITRE,
                    Description = myevent.EVENT_DESCRIPTION,
                    Type = myevent.EVENT_TYPE,
                    StartDate = myevent.EVENT_DATEDEBUT,
                    EndDate = myevent.EVENT_DATEFIN.GetValueOrDefault(),
                    Salle = myevent.T_Location.LOCATION_NAME,
                    Image = myevent.EVENT_IMG_PATH,
                    Email = myevent.EVENT_EMAIL,
                    Tel = myevent.EVENT_TEL,
                    Website = myevent.EVENT_SITE
                };
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }

    }
}