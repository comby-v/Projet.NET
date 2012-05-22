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
        static public bool Create(FormEventModels myevent, DataAccess.T_User user)
        {
            DataAccess.T_Event ev = new DataAccess.T_Event ();
            
            ev.Titre = myevent.Title;
            ev.Type = EventModel.GetEventType(myevent.Type);
            ev.Description = myevent.Description;
            ev.DateDebut = myevent.StartDate;
            ev.DateFin= myevent.EndDate;
            ev.Image = myevent.Image;
            ev.Email = myevent.Email;
            ev.WebSite = myevent.Website;
            ev.Tel = myevent.Phone;

            List<DataAccess.T_Tag> list_tag = new List<DataAccess.T_Tag>();
            List<String> tags = myevent.Tags.Split(new Char[] { ' ', ',', '.', ';'}).ToList();
            foreach (String tag in tags)
            {
                Regex regx = new Regex("[a-z1-9*]");
                if (tag.Length > 2 && regx.Match(tag).Success)
                {
                    tag.ToLower();
                    DataAccess.T_Tag bdd_tag = BusinessManagement.Tag.Get(tag);
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

            DataAccess.T_Location location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
            if (location != null)
            {
                return DataAccess.Event.Create(ev, user, location, list_tag);
            }
            else
            {
                if (BusinessManagement.Location.Create(new DataAccess.T_Location()
                                                        {
                                                            Pays = myevent.Country,
                                                            Ville = myevent.City,
                                                            CP = myevent.CodePostal,
                                                            Rue = myevent.Address,
                                                            Name = myevent.RoomName,
                                                            Latitude = myevent.Latitude,
                                                            Longitude = myevent.Longitude
                                                        }))
                {
                    DataAccess.T_Location n_location = Location.GetLocationByCoord(myevent.Latitude, myevent.Longitude);
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


        static public bool Update(FormEventModels myevent, DataAccess.T_Location location, DataAccess.T_User user, long id)
        {
            DataAccess.T_Event ev = new DataAccess.T_Event();
            ev.Id = id;
            ev.Titre = myevent.Title;
            ev.Type = EventModel.GetEventType(myevent.Type);
            ev.Description = myevent.Description;
            ev.DateDebut = myevent.StartDate;
            ev.DateFin = myevent.EndDate;
            ev.Image = myevent.Image;
            ev.Email = myevent.Email;
            ev.WebSite = myevent.Website;
            ev.Tel = myevent.Phone;
            ev.T_Location = location;
            ev.T_User = user;

            return DataAccess.Event.Update(ev);
        }

        static public DataAccess.T_Event Get(long id, bool creation = false)
        {
            return DataAccess.Event.Get(id, creation);
        }

        static public DataAccess.T_Event Get(string title, bool creation = false)
        {
            return DataAccess.Event.Get(title, creation);
        }

        static public List<EventItem> GetListLastAddEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.T_Event> list_event = DataAccess.Event.GetListLastAddEvent(nbr, type);
            foreach (DataAccess.T_Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Image = myevent.Image,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite
                };
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }


        static public List<EventItem> GetListEvent(int nbr, string type = "")
        {
            List<EventItem> list_eventItem = new List<EventItem>();
            List<DataAccess.T_Event> list_event = DataAccess.Event.GetListEvent(nbr, type);
            foreach (DataAccess.T_Event myevent in list_event)
            {
                EventItem myeventitem = new EventItem()
                {
                    Id = myevent.Id,
                    Titre = myevent.Titre,
                    Description = myevent.Description,
                    Type = myevent.Type,
                    StartDate = myevent.DateDebut,
                    EndDate = myevent.DateFin.GetValueOrDefault(),
                    Salle = myevent.T_Location.Name,
                    Image = myevent.Image,
                    Email = myevent.Email,
                    Tel = myevent.Tel,
                    Website = myevent.WebSite
                };
                list_eventItem.Add(myeventitem);
            }
            return list_eventItem;
        }

    }
}