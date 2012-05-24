using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Notification
    {
        public static bool Create(T_Notification notification, T_User user, T_Event myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Event a_event = bdd.T_Event.Where(x => x.Id == myevent.Id).FirstOrDefault();
                    T_User a_user = bdd.T_User.Where(x => x.Id == user.Id).FirstOrDefault();

                    bdd.Attach(a_event);
                    bdd.Attach(a_user);

                    notification.T_Event.Add(a_event);
                    notification.T_User.Add(a_user);

                    bdd.T_Notification.AddObject(notification);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                   
                }
            }
            return true;
        }

        public static bool Create(T_Notification notification, long id)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_User user = bdd.T_User.Where(x => x.Id == id).FirstOrDefault();
                    bdd.Attach(user);
                    notification.T_User.Add(user);

                    bdd.AddToT_Notification(notification);
                    bdd.SaveChanges();
                }
                catch (Exception)
                {
                    throw;

                }
            }
            return true;
        }

        public static List<DataAccess.T_Notification> Get(T_User user)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                     List<T_Notification> notiflist = bdd.T_Notification.Include("T_User").Include("T_Event").ToList();
                     List<T_Notification> relist = new List<T_Notification>(); 
                    foreach (T_Notification notif in notiflist)
                     {
                         bool find = false;
                         List<T_User> test = notif.T_User.ToList();
                         foreach (T_User us in test)
                         {
                            
                             if (us.Id == user.Id)
                             {
                                 find = true;
                                 break;
                             }
                         }
                         if (find)
                         {
                             relist.Add(notif);
                         }
                     }

                    List<DataAccess.T_Notification> notifs = bdd.T_User.Include("T_Notification").Include("T_Event").Where(x => x.Id == user.Id).FirstOrDefault().T_Notification.ToList();



                    return relist;
                }
                catch (Exception)
                {
                    throw;
                   
                }
            }
        }

        public static bool Deny(T_Notification notif, long idEvent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    T_Event myevent = bdd.T_Event.Include("T_User").Where(x => x.Id == idEvent).FirstOrDefault();
                    notif.T_Event.Add(myevent);
                    notif.T_User.Add(myevent.T_User);
                    bdd.AddToT_Notification(notif);
                    bdd.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                    return false;
                }
            }
        }
    }
}