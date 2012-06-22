using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConcertFinderMVC.Models;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Event
    {
        static public bool Create(T_Event myevent, T_User user, T_Location location, List<T_Tag> tags)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.Attach(user);
                    bdd.Attach(location);
                    foreach (T_Tag tag in tags)
                    {
                        T_Tag a_tag = bdd.T_Tag.Where(x => x.Name == tag.Name).FirstOrDefault();
                        bdd.Attach(a_tag);
                        myevent.T_Tag.Add(a_tag);
                    }

                    myevent.T_Location = location;
                    myevent.T_User = user;

                    bdd.AddToT_Event(myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public bool Delete(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.DeleteObject(bdd.T_Event.Where(x => x.Id == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public bool Update(T_Event myevent, long id, List<T_Tag> taglist)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Location location = Location.GetLocationById(id);
                    //bdd.Attach(myevent);
                    bdd.Attach(location);
                    foreach (T_Tag tag in taglist)
                    {
                        T_Tag a_tag = bdd.T_Tag.Where(x => x.Name == tag.Name).FirstOrDefault();
                        bdd.Attach(a_tag);
                        myevent.T_Tag.Add(a_tag);
                    }

                    myevent.T_Location.Name = location.Name;
                    myevent.T_Location.Latitude = location.Latitude;
                    myevent.T_Location.Longitude = location.Longitude;
                    myevent.T_Location.Pays = location.Pays;
                    myevent.T_Location.Rue = location.Rue;
                    myevent.T_Location.CP = location.CP;
                    myevent.T_Location.Ville = location.Ville;

                    var n_event = new T_Event { Id = myevent.Id };
                    bdd.T_Event.Attach(n_event);
                    
                    bdd.ApplyCurrentValues("T_Event", myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                   throw;
                }
            }
            return true;
        }

        static public bool Update(T_Event myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_event = new T_Event { Id = myevent.Id };
                    bdd.T_Event.Attach(n_event);
                    bdd.ApplyCurrentValues("T_Event", myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public T_Event Get(long id, bool creation = false)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                T_Event myevent;
                try
                {
                    if (!creation)
                    {
                        myevent = bdd.T_Event.Include("T_Tag").Include("T_Location").Include("T_User").Include("T_Notification").
                            Where(x => x.Valide == true && x.DateDebut > DateTime.Now && x.Id == id).FirstOrDefault();
                    }
                    else
                    {
                        myevent = bdd.T_Event.Include("T_Tag").Include("T_Location").Include("T_User").Include("T_Notification").
                            Where(x => x.Id == id).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return myevent;
            }
        }

        static public T_Event Get(string title, bool creation = false)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                T_Event myevent;
                try
                {
                    if (!creation) 
                    {
                        myevent = bdd.T_Event.Where(x => x.Valide == true && x.DateDebut > DateTime.Now && x.Titre == title).FirstOrDefault();
                    }
                    else
                    {
                        myevent = bdd.T_Event.Where(x => x.Titre == title).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return myevent;
            }
        }

        public static List<T_Tag> getTaglistfromEvent(long idEvent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Event eve = bdd.T_Event.Where(x => x.Id == idEvent).FirstOrDefault();
                    List<T_Tag> tags = eve.T_Tag.ToList();
                    return tags;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public List<T_Event> GetListLastAddEvent(int nbr, string type = "")
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    List<T_Event> myevent = bdd.T_Event.Include("T_Location").
                        Where(x => x.Valide == true && x.DateDebut > DateTime.Now).OrderByDescending(x => x.Id).Take(nbr).ToList();
                    return (myevent);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public List<T_Event> GetListEvent(int nbr, string type = "")
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    List<T_Event> myevent;
                    if (type != "")
                    {
                        myevent = bdd.T_Event.Include("T_Location").
                            Where(x => x.Valide == true && x.DateDebut > DateTime.Now && x.Type == type).OrderBy(x => x.DateDebut).Take(nbr).ToList();
                    }
                    else
                    {
                        myevent = bdd.T_Event.Include("T_Location").
                            Where(x => x.Valide == true && x.DateDebut > DateTime.Now).OrderBy(x => x.DateDebut).Take(nbr).ToList();
                    }
                    return (myevent);
                }
                catch (Exception)
                {
                    throw;
                   
                }
            }
        }

        static public List<T_Event> GetListNonValid()
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_Event.Include("T_Location").Where(ev => ev.Valide == false).OrderByDescending(ev => ev.DateCreation).ToList());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public List<T_Event> GetListValid()
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    return (bdd.T_Event.Include("T_Location").Where(ev => (ev.Valide == true) && (ev.DateDebut > DateTime.Now)).OrderByDescending(ev => ev.DateCreation).ToList());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public List<T_Event> MyEvent(string pseudo)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                List<T_Event> list = new List<T_Event>();
                try
                {
                    T_User user = User.GetUserByPseudo(pseudo);
                    if (user != null)
                    {
                        list = bdd.T_Event.Include("T_Location").Include("T_Tag").Where(e => e.Valide == true && e.DateDebut > DateTime.Now).ToList()
                            .Where(x => x.T_Tag.Intersect(user.T_Tag).Count() >= 1 &&
                                user.Ville.Equals(x.T_Location.Ville, StringComparison.InvariantCultureIgnoreCase))
                                .OrderBy(x => x.DateDebut).ToList();
                    }
                    return list;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

         static public bool ValidEvent(long idEvent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Event ev = bdd.T_Event.Include("T_Tag").Include("T_Location").Where(x => x.Id == idEvent).FirstOrDefault();

                    List<T_User> users = bdd.T_User.Include("T_Tag").Where(u => u.Deleted == false).ToList().Where(x => x.T_Tag.Intersect(ev.T_Tag).Count() >= 1 || x.Ville == ev.T_Location.Ville || x.T_Event.Contains(ev)).ToList();

                    foreach (T_User user in users)
                    {
                        T_Notification notif = new T_Notification()
                        {
                            Titre = NotificationModel.GetStatus((int)eStatus.Ajout),
                            Date = DateTime.Now,
                            Message = "L'événement suivant vient d'être ajouté " + ev.Titre,
                            Check = false
                        };
                        notif.T_User.Add(user);
                        notif.T_Event.Add(ev);
                        bdd.AddToT_Notification(notif);
                    }
                    bdd.SaveChanges();
                    ev.Valide = true;
                    return (Update(ev));
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

         static public List<T_Event> GetNextEvents(int last_id, string type)
         {
             using (ConcertFinderEntities bdd = new ConcertFinderEntities())
             {
                 List<T_Event> list = new List<T_Event>();
                 try
                 {
                    T_Event last_event = bdd.T_Event.Where(x => x.Id == last_id).FirstOrDefault();
                    if (type == "")
                    {
                        list = bdd.T_Event.Include("T_Location").Include("T_Tag").Where(e => e.Valide == true && e.DateDebut > DateTime.Now && e.DateDebut > last_event.DateDebut).ToList().OrderBy(x => x.DateDebut).Take(5).ToList();
                    }
                    else
                    {
                        list = bdd.T_Event.Include("T_Location").Include("T_Tag").Where(e => e.Valide == true && e.DateDebut > DateTime.Now && e.DateDebut > last_event.DateDebut && e.Type == type).ToList().OrderBy(x => x.DateDebut).Take(5).ToList();
                    }
                    return list;
                 }
                 catch (Exception)
                 {
                     return null;
                 }
             }
         }

         static public List<T_Event> GetEventForAdmin(T_Event myevent, int nb)
         {         
             using (ConcertFinderEntities bdd = new ConcertFinderEntities ())
             {
                 List<T_Event> listevent = new List<T_Event>();
                 listevent = bdd.T_Event.Include("T_Location").Include("T_Tag").Where(ev => (ev.DateDebut == myevent.DateDebut) &&
                     (ev.T_Location.Name == myevent.T_Location.Name) && (ev.Id != myevent.Id)).OrderBy(ev => ev.DateDebut).ToList();

                 if (nb > listevent.Count)
                 {
                     return listevent;
                 }
                 
                 return listevent.Take(nb).ToList();
             }
         }

         static public List<T_Event> GetListEventByUserTag(T_Event myevent, T_User user, int nb)
         {
             using(ConcertFinderEntities bdd = new ConcertFinderEntities ())
             {
                List<T_Event> listEvent = new List<T_Event>();
                List<T_Event> listRes = new List<T_Event>();

                listEvent = bdd.T_Event.Include("T_Location").Include("T_Tag").Include("T_User").Where(ev => ((ev.Type == myevent.Type) &&
                    (ev.T_Location.Ville == myevent.T_Location.Ville) && (ev.DateDebut >= DateTime.Now ) && (ev.Id != myevent.Id))).OrderBy(ev => ev.DateDebut).ToList();
                foreach (T_Event event2 in listEvent)
                {
                    bool broke = false;
                    foreach (T_Tag tag in event2.T_Tag)
                    {
                        foreach (T_Tag tagevent in myevent.T_Tag)
                        {
                            if (tagevent.Name.Equals(tag.Name))
                            {
                                listRes.Add(event2);
                                broke = true;
                                break;
                            }
                        }
                        if (broke)
                        {
                            break;
                        }
                    }
                }
                if (listRes.Count < nb)
                {
                    return listRes;

                }
             return listRes.Take(nb).ToList();
            }
         }

    }
}