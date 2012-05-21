using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ConcertFinderMVC.DataAccess
{
    public partial class Notification
    {
        public static bool Create(Notification notification, User user, Event myevent)
        {
            using (ConcertFinderEntities bdd = new ConcertFinderEntities())
            {
                try
                {
                    Event a_event = bdd.T_Event.Where(x => x.EVENT_ID == myevent.EVENT_ID).FirstOrDefault();
                    User a_user = bdd.T_User.Where(x => x.Id == user.Id).FirstOrDefault();

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
    }
}