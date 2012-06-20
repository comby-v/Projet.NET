using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.DataAccess;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC.BusinessManagement
{
    public class Tag
    {
        public static bool Create(string tagname)
        {
            DataAccess.T_Tag tag = new DataAccess.T_Tag() { Name = tagname };

            return DataAccess.Tag.Create(tag);
        }

        public static DataAccess.T_Tag Get(string tagName)
        {
            return DataAccess.Tag.Get(tagName);
        }

        public static List<EventItem> Search(string q)
        {
            List<EventItem> list_item = new List<EventItem>();
            List<T_Event> events = DataAccess.Tag.Search(q);
            foreach (T_Event ev in events)
            {
                EventItem item = new EventItem()
                {
                    CP = ev.T_Location.CP,
                    Ville = ev.T_Location.Ville,
                    Salle = ev.T_Location.Name,
                    Type = ev.Type,
                    Description = ev.Description,
                    Email = ev.Email,
                    Id = ev.Id,
                    Titre = ev.Titre,
                    StartDate = ev.DateDebut
                };
                Event.ServerPathImage(ev, item);
                list_item.Add(item);
            }
            return list_item;
        }
    }
}