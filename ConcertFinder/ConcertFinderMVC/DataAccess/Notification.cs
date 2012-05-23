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
                    List<DataAccess.T_Notification> notifs = bdd.T_User.Include("T_Notification").Where(x => x.Id == user.Id).FirstOrDefault().T_Notification.ToList();
                    return notifs;
                }
                catch (Exception)
                {
                    throw;
                   
                }
            }
        }
    }
}