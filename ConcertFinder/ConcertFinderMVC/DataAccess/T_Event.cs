﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public class T_Event
    {
        static public bool Create(EVENT myevent, USER user, LOCATION location)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    bdd.Attach(user);
                    bdd.Attach(location);

                    myevent.LOCATION = location;
                    myevent.USER = user;

                    bdd.AddToEVENTs(myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
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
                    bdd.DeleteObject(bdd.EVENTs.Where(x => x.EVENT_ID == id).FirstOrDefault());
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }



        static public bool Update(EVENT myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    var n_event = new EVENT { EVENT_ID = myevent.EVENT_ID };
                    bdd.EVENTs.Attach(n_event);
                    bdd.ApplyCurrentValues("EVENT", myevent);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        static public EVENT Get(long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                EVENT myevent;
                try
                {
                    myevent = bdd.EVENTs.Include("TAG").Include("LOCATION").Include("USER").Include("NOTIFICATION").
                        Where(x => x.EVENT_ID == id).FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
                return myevent;
            }
        }

        public static List<TAG> getTaglistfromEvent(long idEvent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    EVENT eve = bdd.EVENTs.Where(x => x.EVENT_ID == idEvent).FirstOrDefault();
                    List<TAG> tags = eve.TAGs.ToList();
                    return tags;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}