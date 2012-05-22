using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Event
    {
        static public bool Create(Event myevent, User user, Location location, List<Tag> tags)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.Attach(user);
                    bdd.Attach(location);
                    foreach (Tag tag in tags)
                    {
                        bdd.Attach(tag);
                        myevent.T_Tag.Add(tag);
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
                    bdd.DeleteObject(bdd.T_Event.Where(x => x.EVENT_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }



        static public bool Update(Event myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_event = new Event { EVENT_ID = myevent.EVENT_ID };
                    bdd.T_Event.Attach(n_event);
                    bdd.ApplyCurrentValues("T_EVENT", myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public Event Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                Event myevent;
                try
                {
                    myevent = bdd.T_Event.Include("T_Tag").Include("T_Location").Include("T_User").Include("T_Notification").
                        Where(x => x.EVENT_VALIDE == true && x.EVENT_DATEDEBUT > DateTime.Now && x.EVENT_ID == id).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return myevent;
            }
        }

        static public Event Get(string title, bool creation = false)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                Event myevent;
                try
                {
                    if (!creation)
                    {
                        myevent = bdd.T_Event.Where(x => x.EVENT_VALIDE == true && x.EVENT_DATEDEBUT > DateTime.Now && x.EVENT_TITRE == title).FirstOrDefault();
                    }
                    else
                    {
                        myevent = bdd.T_Event.Where(x => x.EVENT_TITRE == title).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return myevent;
            }
        }

        public static List<Tag> getTaglistfromEvent(long idEvent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Event eve = bdd.T_Event.Where(x => x.EVENT_ID == idEvent).FirstOrDefault();
                    List<Tag> tags = eve.T_Tag.ToList();
                    return tags;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        static public List<Event> GetListLastAddEvent(int nbr, string type = "")
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    List<Event> myevent = bdd.T_Event.Include("T_Location").
                        Where(x => x.EVENT_VALIDE == true && x.EVENT_DATEDEBUT > DateTime.Now).OrderByDescending(x => x.EVENT_ID).Take(nbr).ToList();
                    return (myevent);
                }
                catch (Exception)
                {
                    throw;
                    
                }
            }
        }

        static public List<Event> GetListEvent(int nbr, string type = "")
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    List<Event> myevent;
                    if (type != "")
                    {
                        myevent = bdd.T_Event.Include("T_Location").
                            Where(x => x.EVENT_VALIDE == true && x.EVENT_DATEDEBUT > DateTime.Now && x.EVENT_TYPE == type).OrderBy(x => x.EVENT_DATEDEBUT).Take(nbr).ToList();
                    }
                    else
                    {
                        myevent = bdd.T_Event.Include("T_Location").
                            Where(x => x.EVENT_VALIDE == true && x.EVENT_DATEDEBUT > DateTime.Now).OrderBy(x => x.EVENT_DATEDEBUT).Take(nbr).ToList();
                    }
                    return (myevent);
                }
                catch (Exception)
                {
                    throw;
                   
                }
            }
        }
    }
}